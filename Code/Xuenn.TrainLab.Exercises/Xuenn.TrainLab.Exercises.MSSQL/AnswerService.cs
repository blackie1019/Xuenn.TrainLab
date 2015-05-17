using Xuenn.TrainLab.Exercises.MSSQL.Entities;
using Xuenn.TrainLab.Exercises.MSSQL.Enums;

namespace Xuenn.TrainLab.Exercises.MSSQL
{
    public class AnswerService
    {
        private AnswerServiceDAL _serviceDAL = AnswerServiceDAL.Instance;
        public QuizResult VerifyAnswer(QuizAnswer answer)
        {
            var result = new QuizResult();
            switch (answer.SQLVerifyType)
            {
                case SQLVerifyType.Check:
                    result = _serviceDAL.VerifyAnswerWithCheck(answer);
                    break;
                case SQLVerifyType.Trigger:
                    result = _serviceDAL.VerifyAnswerWithTrigger(answer);
                    break;
                case SQLVerifyType.Update:
                    result = _serviceDAL.VerifyAnswerWithUpdate(answer);
                    break;
                case SQLVerifyType.Alter:
                    result = _serviceDAL.VerifyAnswerWithAlter(answer);
                    break;
                case SQLVerifyType.Delete:
                    result = _serviceDAL.VerifyAnswerWithDelete(answer);
                    break;
            }
            return result;
        }
    }
}
