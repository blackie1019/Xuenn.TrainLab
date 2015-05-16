using System.Runtime.Serialization;
namespace Xuenn.TrainLab.Site.Models
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