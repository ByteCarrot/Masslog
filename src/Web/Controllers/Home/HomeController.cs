using System.Web.Mvc;
using ByteCarrot.Masslog.Core.DataAccess;
using ByteCarrot.Masslog.Web.Infrastructure;

namespace ByteCarrot.Masslog.Web.Controllers.Home
{
    public partial class HomeController : MvcController
    {
        private readonly IMongoDatabaseInitializer _initializer;

        public HomeController(IMongoDatabaseInitializer initializer)
        {
            _initializer = initializer;
        }

        [Authorize, AppendLayoutViewModel]
        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult Reset()
        {
            _initializer.Initialize();
            return RedirectToAction(MVC.Home.ActionNames.Index);
        }
    }
}
