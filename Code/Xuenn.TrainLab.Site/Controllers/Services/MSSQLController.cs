using System.Web.Http;
using Xuenn.TrainLab.Site.ViewModels;
using Xuenn.TrainLab.Web.BLL;
using Xuenn.TrainLab.Web.Models;

namespace Xuenn.TrainLab.Site.Controllers.Services
{
    public class MSSQLController : ApiController
    {
        private MSSQLBLL BLL=new MSSQLBLL();
        [HttpPost]
        public int VerifyAnwser(AnswerViewModel viewModel)
        {
            return BLL.VerifyAnswer(new AnswerModel
            {
                QuizNumber = viewModel.QuizNumber,
                VerifySql = viewModel.VerifySql
            });
        }
    }
}