using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ByteCarrot.Masslog.Web.Controllers.Activities;
using ByteCarrot.Masslog.Core.DomainModel.Entities;
using ByteCarrot.Masslog.Web.Infrastructure;

namespace ByteCarrot.Masslog.Web.Controllers.Activities
{
    [CanAccessActivity]
    public partial class ActivityController : MvcController
    {
        public virtual ActionResult Index(string id)
        {
            var activity = Context.Activities.Get(id);
            if (activity == null)
            {
                return HttpNotFound();
            }

            CurrentActivity = activity;
            var model = new ActivityViewModel {HasRouteData = CurrentActivity.RouteDataIgnored || CurrentActivity.RouteData != null};

            return PartialView(model);
        }

        public virtual ActionResult Details()
        {
            return PartialView(DetailsViewModel.Create(CurrentActivity));
        }

        public new virtual ActionResult Request()
        {
            return PartialView(RequestViewModel.Create(CurrentActivity.Request));
        }

        public new virtual ActionResult Response()
        {
            return PartialView(ResponseViewModel.Create(CurrentActivity.Response));
        }

        public virtual ActionResult Exceptions()
        {
            var model = new ExceptionsViewModel
                            {
                                Id = CurrentActivity.Id, 
                                Exceptions = new List<ExceptionViewModel>()
                            };

            if (CurrentActivity.Exceptions != null && CurrentActivity.Exceptions.Count > 0)
            {
                model.Exceptions = ExceptionViewModel.Create(CurrentActivity.Exceptions);
            }
            return PartialView(model);
        }

        public virtual ActionResult RouteData()
        {
            return PartialView(RouteDataViewModel.Create(CurrentActivity));
        }

        public virtual ActionResult ServerVariables()
        {
            var model = new ServerVariablesViewModel
                            {
                                Id = CurrentActivity.Id, 
                                VariablesIgnored = CurrentActivity.VariablesIgnored,
                                Variables = new List<NameValueListViewModel>()
                            };

            if (CurrentActivity.Variables != null && CurrentActivity.Variables.Count > 0)
            {
                model.Variables = CurrentActivity.Variables.Select(x => new NameValueListViewModel(x.Name, x.Value)).ToList();
            }
            return PartialView(model);
        }

        private Activity CurrentActivity
        {
            get { return Session.Get<ActivityState>().Activity; }
            set { Session.Set(new ActivityState { Activity = value }); }
        }
    }
}