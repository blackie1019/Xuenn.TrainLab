using Xuenn.TrainLab.Exercises.MSSQL.Enums;

namespace Xuenn.TrainLab.Exercises.MSSQL.Entities
{
    public class QuizResult
    {
        public QuizResult()
        {
            ResultType = ResultType.Exception;
            Description = string.Empty;
        }
        public ResultType ResultType;
        public string Description;
    }
}
