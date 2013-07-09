using System;
using System.Linq;
using System.Web.Mvc;
using ByteCarrot.Masslog.Core.DomainModel.Repositories;
using ByteCarrot.Masslog.Web.Infrastructure;

namespace ByteCarrot.Masslog.Web.Controllers.Activities
{
    [Authorize]
    public partial class ActivitiesController : MvcController
    {
        [HttpGet]
        public virtual PartialViewResult Index()
        {
            var query = Session.Get<ActivityQuery>();
            if (query == null)
            {
                query = ActivityQuery.Default();
                Session.Set(query);
            }

            var applications = Context.Applications.In(Security.ReadAccess).OrderBy(x => x.Name);

            return PartialView(MVC.Activities.Views.Index, ActivitiesViewModel.Create(query, applications));
        }

        [HttpGet]
        public virtual PartialViewResult LastSearch()
        {
            return Search(Session.Get<ActivityQuery>());
        }

        [HttpGet]
        public virtual RedirectToRouteResult DateRange(DateRange range)
        {
            var query = Session.Get<ActivityQuery>();
            query.DateFrom = DateRangeToDateTime(range);
            query.DateTo = null;

            return RedirectToAction(MVC.Activities.ActionNames.Index);
        }

        [HttpPost]
        public virtual PartialViewResult Search(ActivitiesViewModel model)
        {
            var query = Session.Get<ActivityQuery>();
            model.UpdateQuery(query);
            query.PageNumber = 1;
            
            return Search(query);
        }

        [HttpGet]
        public virtual PartialViewResult Page(int page)
        {
            var state = Session.Get<ActivityQuery>();
            state.PageNumber = page;

            return Search(state);
        }

        [HttpGet]
        public virtual PartialViewResult Sort(string column, string direction)
        {
            var query = Session.Get<ActivityQuery>();
            query.SortColumn = column;
            query.SortDirection = (SortDirection) Enum.Parse(typeof (SortDirection), direction);

            return Search(query);
        }

        [HttpGet]
        public virtual RedirectToRouteResult Clear()
        {
            Session.Set<ActivityQuery>(null);
            return RedirectToAction(MVC.Activities.ActionNames.Index);
        }

        private PartialViewResult Search(ActivityQuery query)
        {
            var actions = Context.Activities.Search(query);
            var model = SearchResultViewModel.Create(query, actions);

            return PartialView(MVC.Activities.ActionNames.Search, model);
        }

        private static DateTime DateRangeToDateTime(DateRange range)
        {
            switch (range)
            {
                case Activities.DateRange.Last5Minutes:
                    return DateTime.Now.AddMinutes(-5);
                case Activities.DateRange.Last1Hour:
                    return DateTime.Now.AddHours(-1);
                case Activities.DateRange.Last24Hours:
                    return DateTime.Now.AddDays(-1);
                case Activities.DateRange.Last7Days:
                    return DateTime.Now.AddDays(-7);
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}