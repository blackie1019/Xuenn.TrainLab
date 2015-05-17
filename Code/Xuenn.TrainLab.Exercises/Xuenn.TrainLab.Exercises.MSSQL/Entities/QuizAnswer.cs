using Xuenn.TrainLab.Exercises.MSSQL.Enums;

namespace Xuenn.TrainLab.Exercises.MSSQL.Entities
{
    public class QuizAnswer
    {
        public int QuizNumber;
        public SQLVerifyType SQLVerifyType;
        public string VerifySql;
        public string ConnectionString;
    }
}
