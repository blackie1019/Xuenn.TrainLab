using System.Web.Http;
using Xuenn.TrainLab.Site.ViewModels;

namespace Xuenn.TrainLab.Site.Controllers.Services
{
    public class TestController : ApiController
    {
        [HttpGet]
        public TestModel Index()
        {
            var result = new TestModel
            {
                Name = "Blackie",
                Value = "Okok"
            };
            return result;
        }
    }
}