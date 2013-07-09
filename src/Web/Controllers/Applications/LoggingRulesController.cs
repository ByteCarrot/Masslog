using ByteCarrot.Masslog.Core.Infrastructure.Extensions;
using System.Web.Mvc;
using ByteCarrot.Masslog.Web.Controllers.Applications;
using ByteCarrot.Masslog.Core.Rules;
using ByteCarrot.Masslog.Web.Infrastructure;

namespace ByteCarrot.Masslog.Web.Controllers.Applications
{
    [Authorize]
    public partial class LoggingRulesController : MvcController
    {
        private readonly IRulesParser _parser;

        public LoggingRulesController(IRulesParser parser)
        {
            _parser = parser;
        }

        [HttpGet, CanAccessApplication]
        public virtual ActionResult Index()
        {
            var state = Session.Get<ApplicationState>();
            var model = new LoggingRulesViewModel
                            {
                                Rules = GetLoggingRules(state.ApplicationId)
                            };

            return Security.CanModifyApplication(state.ApplicationId)
                ? PartialView(model)
                : PartialView(MVC.LoggingRules.Views.ReadOnly, model);
        }

        [HttpPost, CanModifyApplication]
        public virtual ActionResult Verify(string rules)
        {
            return rules.Empty()
                ? VerifyTransferObject.EmptyRules().ToResult()
                : VerifyTransferObject.Create(_parser.Parse(rules)).ToResult();
        }

        [HttpPost, CanModifyApplication]
        public virtual ActionResult Cancel()
        {
            var state = Session.Get<ApplicationState>();
            return new CancelTransferObject
                       {
                           Success = true, 
                           Rules = GetLoggingRules(state.ApplicationId)
                       }
                       .ToResult();
        }

        [HttpPost, CanModifyApplication]
        public virtual ActionResult Save(string rules)
        {
            if (rules.Empty())
            {
                VerifyTransferObject.EmptyRules().ToResult();
            }

            var result = _parser.Parse(rules);
            if (!result.Success)
            {
                return VerifyTransferObject.Create(result).ToResult();
            }

            var state = Session.Get<ApplicationState>();
            var application = Context.Applications.Get(state.ApplicationId);
            application.LoggingRules = rules;
            Context.Applications.Save(application);

            return new SaveTransferObject { Success = true }.ToResult();
        }

        private string GetLoggingRules(string applicationId)
        {
            var application = Context.Applications.Get(applicationId);
            return application.LoggingRules;
        }
    }
}