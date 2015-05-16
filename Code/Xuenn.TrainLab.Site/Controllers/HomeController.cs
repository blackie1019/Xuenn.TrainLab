using System.Web.Http;
using Xuenn.TrainLab.Site.Models;

namespace Xuenn.TrainLab.Site.Controllers
{
    public class HomeController : ApiController
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