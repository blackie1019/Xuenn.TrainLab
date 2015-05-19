using Xuenn.TrainLab.Exercises.MSSQL;
using Xuenn.TrainLab.Exercises.MSSQL.Entities;
using Xuenn.TrainLab.Exercises.MSSQL.Enums;
using Xuenn.TrainLab.Web.Models;

namespace Xuenn.TrainLab.Web.BLL
{
    public class MSSQLBLL
    {
        private static readonly AnswerService Service= new AnswerService();

        public AnswerResultModel VerifyAnswer(AnswerModel model)
        {
            var quizResult = Service.VerifyAnswer(new QuizAnswer
            {
                QuizNumber = model.QuizNumber,
                VerifySql = model.VerifySql,
                SQLVerifyType = GetSQLVerifyType(model.QuizNumber),
                ConnectionString = "Data source=.;Initial Catalog=NorthwindChinese;User id=sa;Password=pass.123"
            });

            var result = new AnswerResultModel
            {
                QuizNumber = model.QuizNumber,
                VerifySql = model.VerifySql,
                IsPassedVerifiy = quizResult.ResultType==ResultType.Success,
                VerifiyResultDescription = ConvertVerifyResultToDescriptPromptOut(quizResult.ResultType)
            };

            return result;
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

        private string ConvertVerifyResultToDescriptPromptOut(ResultType type)
        {
            return type.ToString();
        }

        #endregion
    }
}
