using System.Runtime.Serialization;

namespace Xuenn.TrainLab.Site.ViewModels
{
    [DataContract]
    public class ResultViewModel
    {
        [DataMember] 
        public int QuizNumber;

        [DataMember] 
        public string VerifySql;

        [DataMember] 
        public bool IsPassedVerifiy;

        [DataMember]
        public string VerifiyResultDescription;

    }
}