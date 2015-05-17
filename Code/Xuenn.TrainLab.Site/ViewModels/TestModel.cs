using System.Runtime.Serialization;
namespace Xuenn.TrainLab.Site.ViewModels
{
    [DataContract]
    public class TestModel
    {
        [DataMember]
        public string Name;
        [DataMember]
        public string Value;
    }
}