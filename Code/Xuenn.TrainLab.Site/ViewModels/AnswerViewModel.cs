using System.Runtime.Serialization;

namespace Xuenn.TrainLab.Site.ViewModels
{
    [DataContract]
    public class AnswerViewModel
    {
        [DataMember] 
        public int QuizNumber;

        [DataMember] 
        public string VerifySql;
    }
}