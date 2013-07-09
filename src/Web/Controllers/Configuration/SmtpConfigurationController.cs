using ByteCarrot.Masslog.Web.Infrastructure;
using System.Web.Mvc;

namespace ByteCarrot.Masslog.Web.Controllers.Configuration
{
    public partial class SmtpConfigurationController : MvcController
    {
        private readonly ISmtpConfigurationViewModelAssembler _assembler;

        public SmtpConfigurationController(ISmtpConfigurationViewModelAssembler assembler)
        {
            _assembler = assembler;
        }

        [HttpGet]
        public virtual ActionResult Index()
        {
            return PartialView(_assembler.ToViewModel(Context.Configuration.GetSmtpConfiguration()));
        }

        [HttpGet]
        public virtual ActionResult Edit()
        {
            return PartialView(_assembler.ToViewModel(Context.Configuration.GetSmtpConfiguration()));
        }

        [HttpPost]
        public virtual ActionResult Edit(SmtpConfigurationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(model);
            }

            return PartialView();
        }
    }
}