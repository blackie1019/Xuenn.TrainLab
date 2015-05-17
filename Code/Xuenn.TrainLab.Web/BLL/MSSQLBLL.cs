using Xuenn.TrainLab.Exercises.MSSQL;
using Xuenn.TrainLab.Exercises.MSSQL.Entities;
using Xuenn.TrainLab.Exercises.MSSQL.Enums;
using Xuenn.TrainLab.Web.Models;

namespace Xuenn.TrainLab.Web.BLL
{
    public class MSSQLBLL
    {
        private static readonly AnswerService Service= new AnswerService();

        public int VerifyAnswer(AnswerModel model)
        {
            var result = Service.VerifyAnswer(new QuizAnswer
            {
                QuizNumber = model.QuizNumber,
                VerifySql = model.VerifySql,
                SQLVerifyType = GetSQLVerifyType(model.QuizNumber),
                ConnectionString = "Data source=.;Initial Catalog=NorthwindChinese;User id=sa;Password=pass.123"
            });

            return (int)result.ResultType;
        }

        #region private

        private SQLVerifyType GetSQLVerifyType(int quizNumber)
        {
            switch (quizNumber)
            {
                case 22:
                    return SQLVerifyType.Trigger;
                case 23:
                    return SQLVerifyType.Update;
                case 24:
                    return SQLVerifyType.Alter;
                case 25:
                    return SQLVerifyType.Delete;
                default:
                    return SQLVerifyType.Check;
            }
        }

        #endregion
    }
}
