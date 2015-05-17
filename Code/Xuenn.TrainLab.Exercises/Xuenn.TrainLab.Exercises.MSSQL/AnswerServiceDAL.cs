using System;
using System.Data.SqlClient;
using Xuenn.TrainLab.Exercises.MSSQL.Entities;
using Xuenn.TrainLab.Exercises.MSSQL.Enums;

namespace Xuenn.TrainLab.Exercises.MSSQL
{
    public class AnswerServiceDAL
    {
        private static AnswerServiceDAL _instance;

        public static AnswerServiceDAL Instance
        {
            get { return _instance ?? (_instance = new AnswerServiceDAL()); }
        }

        public QuizResult VerifyAnswerWithCheck(QuizAnswer answer)
        {
            var result = new QuizResult();

            if (string.IsNullOrEmpty(answer.VerifySql))
            {
                result.ResultType = ResultType.FailedScriptEmpty;
                return result;
            }
            //connect to database
            var connection = new SqlConnection(answer.ConnectionString);
            connection.Open();

            //connect to database
            var connection2 = new SqlConnection(answer.ConnectionString);
            connection2.Open();

            var correctSql = string.Format("{0}{1}", "SELECT * FROM Answers.dbo.Ans", answer.QuizNumber);
            SqlDataReader readerCorrect = ExecuteReader(correctSql, connection);
            SqlDataReader readerVerify = ExecuteReader(answer.VerifySql, connection2);
            result.ResultType = ResultType.Success;

            //check if two table are the same
            while (true)
            {
                bool b1 = readerVerify.Read();
                bool b2 = readerCorrect.Read();
                if (!(b1 || b2)) break;
                if (b1 ^ b2)
                {
                    result.ResultType = ResultType.FailedScriptInconsistent;
                    break;
                }
                if (readerVerify.FieldCount != readerCorrect.FieldCount)
                {
                    result.ResultType = ResultType.FailedScriptWrongColumnNumber;
                    break;
                }
                for (int i = 0; i < readerCorrect.FieldCount; i++)
                {
                    if (String.Format("{0}", readerVerify[i]) != String.Format("{0}", readerCorrect[i]))
                    {
                        result.ResultType = ResultType.FailedScriptInconsistent;
                        break;
                    }
                }

                if (result.ResultType == ResultType.FailedScriptWrongColumnNumber ||
                    result.ResultType == ResultType.FailedScriptInconsistent)
                {
                    break;
                }
            }
            readerVerify.Close();
            readerCorrect.Close();
            connection.Close();
            connection2.Close();
            return result;
        }

        public QuizResult VerifyAnswerWithTrigger(QuizAnswer answer)
        {
            var result = new QuizResult();

            var connection = new SqlConnection(answer.ConnectionString);

            //reset
            var sqlClear = string.Format("{0}\n{1}\n{2}\n{3}",
                "IF EXISTS (SELECT * FROM sys.triggers WHERE name = 'shippers_trigger')",
                "DROP TRIGGER shippers_trigger;",
                "DELETE FROM ShippersLog;",
                "DBCC CHECKIDENT('dbo.ShippersLog', RESEED, 0)");

            ExecuteNonQuery(sqlClear, connection);

            if (string.IsNullOrEmpty(answer.VerifySql))
            {
                result.ResultType = ResultType.FailedResetCompletely;
                return result;
            }

            //excute the sql to be checked
            SqlSplitExec(answer.VerifySql, connection);

            //test trigger!!
            //exist
            ExecuteNonQuery("INSERT INTO Shippers(CompanyName, Phone) VALUES ('test', '0')", connection);
            ExecuteNonQuery("UPDATE Shippers SET Phone = '1234' WHERE CompanyName = 'test'", connection);
            ExecuteNonQuery("DELETE FROM Shippers WHERE CompanyName = 'test'", connection);
            //not exist
            ExecuteNonQuery("DELETE FROM Shippers WHERE CompanyName = 'empty'", connection);
            ExecuteNonQuery("UPDATE Shippers SET Phone = 'jjj' WHERE CompanyName = 'empty'", connection);
            //reset
            ExecuteNonQuery("DBCC CHECKIDENT('dbo.Shippers', RESEED, 3)", connection);

            //test
            result.ResultType = ResultType.Success;

            string testSql = "(SELECT * FROM Answers.dbo.Ans22 EXCEPT SELECT id, Operation,DelShipperId,DelCompanyName,DelPhone,InsShipperId,InsCompanyName,InsPhone FROM ShippersLog) ";
            testSql += "UNION (SELECT id, Operation,DelShipperId,DelCompanyName,DelPhone,InsShipperId,InsCompanyName,InsPhone FROM ShippersLog EXCEPT SELECT * FROM Answers.dbo.Ans22)";
            var reader = ExecuteReader(testSql, connection);
            if (reader.Read())
            {
                result.ResultType = ResultType.FailedScriptInconsistent;
            }
            reader.Close();

            //clear
            ExecuteNonQuery(sqlClear, connection);
            connection.Close();

            return result;
        }

        public QuizResult VerifyAnswerWithUpdate(QuizAnswer answer)
        {
            var result = new QuizResult();

            var connection = new SqlConnection(answer.ConnectionString);
            //reset
            var sqlClear = "UPDATE Employees SET Salary = ( SELECT Salary FROM Answers.dbo.EmployeesBK BK WHERE Employees.EmployeeID = BK.EmployeeID)";
            ExecuteNonQuery(sqlClear, connection);

            if (string.IsNullOrEmpty(answer.VerifySql))
            {
                result.ResultType = ResultType.FailedResetCompletely;
                return result;
            }

            //excute the sql to be checked
            SqlSplitExec(answer.VerifySql, connection);

            //test
            result.ResultType = ResultType.Success;

            var  testSql = "SELECT * FROM Answers.dbo.Ans23 EXCEPT SELECT EmployeeID, Salary FROM Employees";
            var reader = ExecuteReader(testSql, connection);
            if (reader.Read())
            {
                result.ResultType = ResultType.FailedScriptInconsistent;
            }
            reader.Close();

            //reset
            ExecuteNonQuery(sqlClear, connection);
            connection.Close();

            return result;
        }

        public QuizResult VerifyAnswerWithAlter(QuizAnswer answer)
        {
            var result = new QuizResult();

            var connection = new SqlConnection(answer.ConnectionString);

            //reset
            string sqlClear = string.Format("{0} {1}",
                "IF EXISTS (SELECT column_name FROM INFORMATION_SCHEMA.columns WHERE table_name = 'Employees' and column_name = 'Seniority')",
                "BEGIN ALTER TABLE Employees DROP COLUMN Seniority END");

            ExecuteNonQuery(sqlClear, connection);

            if (string.IsNullOrEmpty(answer.VerifySql))
            {
                result.ResultType = ResultType.FailedResetCompletely;
                return result;
            }

            //excute the sql to be checked
            SqlSplitExec(answer.VerifySql, connection);

            //test
            result.ResultType = ResultType.Success;

            var testSql = "SELECT * FROM Answers.dbo.Ans24 EXCEPT SELECT EmployeeID, Seniority FROM Employees";
            var reader = ExecuteReader(testSql, connection);
            if (reader.Read())
            {
                result.ResultType = ResultType.FailedScriptInconsistent;
            }
            reader.Close();

            //reset
            ExecuteNonQuery(sqlClear, connection);
            connection.Close();

            return result;
        }

        public QuizResult VerifyAnswerWithDelete(QuizAnswer answer)
        {
            var result = new QuizResult();

            var connection = new SqlConnection(answer.ConnectionString);

            //reset
            var sqlClear = string.Format("{0}\n{1}\n{2} {3} {4}",
                "DELETE FROM Products",
                "DBCC CHECKIDENT('dbo.Products', RESEED, 0)",
                "INSERT INTO Products(ProductName,SupplierID,CategoryID,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued)",
                "SELECT ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued",
                "FROM Answers.dbo.ProductsBK");

            ExecuteNonQuery(sqlClear, connection);
            if (string.IsNullOrEmpty(answer.VerifySql))
            {
                result.ResultType = ResultType.FailedResetCompletely;
                return result;
            }

            //excute the sql to be checked
            SqlSplitExec(answer.VerifySql, connection);

            //test
            result.ResultType = ResultType.Success;

            var testSql = string.Format("{0} {1}",
                "(SELECT * FROM Answers.dbo.Ans25 EXCEPT SELECT ProductID FROM Products)",
                "UNION (SELECT ProductID FROM Products EXCEPT SELECT * FROM Answers.dbo.Ans25)");

            var reader = ExecuteReader(testSql, connection);

            if (reader.Read())
            {
                result.ResultType = ResultType.FailedScriptInconsistent;
            }
            reader.Close();

            //reset
            ExecuteNonQuery(sqlClear, connection);
            connection.Close();
            return result;
        }

        #region private

        private SqlDataReader ExecuteReader(string sql, SqlConnection connection)
        {
            var command = new SqlCommand(sql, connection);
            return command.ExecuteReader();
        }

        private void ExecuteNonQuery(string sql, SqlConnection connection)
        {
            var command = new SqlCommand(sql, connection);
            command.ExecuteNonQuery();
        }

        private void SqlSplitExec(string checkSql, SqlConnection connection)
        {
            string[] splitSql = { "GO" };
            splitSql = checkSql.ToUpper().Split(splitSql, StringSplitOptions.RemoveEmptyEntries);
            foreach (var t in splitSql)
            {
                ExecuteNonQuery(t, connection);
            }
        }

        #endregion
    }
}
