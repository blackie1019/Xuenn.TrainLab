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
        public ResultViewModel VerifyAnwser(AnswerViewModel viewModel)
        {
            var model = BLL.VerifyAnswer(new AnswerModel
            {
                QuizNumber = viewModel.QuizNumber,
                VerifySql = viewModel.VerifySql
            });

            var result = new ResultViewModel
            {
                QuizNumber = model.QuizNumber,
                VerifySql = model.VerifySql,
                IsPassedVerifiy = model.IsPassedVerifiy,
                VerifiyResultDescription = model.VerifiyResultDescription
            };
            return result;
        }
    }
}