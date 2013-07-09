using ByteCarrot.Masslog.Core.Infrastructure.Extensions;
using System;
using System.Linq;
using System.Security;
using System.Web.Mvc;
using ByteCarrot.Masslog.Web.Controllers.Applications;
using ByteCarrot.Masslog.Core.DomainModel.Entities;
using ByteCarrot.Masslog.Core.DomainModel.Repositories;
using ByteCarrot.Masslog.Web.Infrastructure;
using ByteCarrot.Masslog.Web.Infrastructure.Security;

namespace ByteCarrot.Masslog.Web.Controllers.Applications
{
    [Authorize]
    public partial class ApplicationController : MvcController
    {
        [HttpGet, CanAccessApplication]
        public virtual ActionResult Index(string id)
        {
            var application = Context.Applications.Get(id);
            if (application == null)
            {
                throw new SecurityException("Application not found.");
            }

            Session.Set(new ApplicationState { ApplicationId = id });

            return PartialView(new ApplicationViewModel { Name = application.Name });
        }

        [HttpGet, CanManageApplications]
        public virtual ActionResult List()
        {
            return List(new ApplicationQuery
            {
                PageNumber = 1,
                PageSize = 10,
                SortColumn = Core.Infrastructure.Name.Of<Application, string>(x => x.Name),
                SortDirection = SortDirection.Ascending
            });
        }

        [HttpGet, CanManageApplications]
        public virtual ActionResult Page(int page)
        {
            var query = Session.Get<ApplicationQuery>();
            query.PageNumber = page;

            return List(query);
        }

        [HttpGet, CanManageApplications]
        public virtual PartialViewResult Sort(string column, string direction)
        {
            var query = Session.Get<ApplicationQuery>();
            query.SortColumn = column;
            query.SortDirection = (SortDirection)Enum.Parse(typeof(SortDirection), direction);

            return List(query);
        }

        [HttpGet, CanManageApplications]
        public virtual ActionResult Add()
        {
            return PartialView(new AddViewModel());
        }

        [HttpPost, CanManageApplications]
        public virtual ActionResult Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(model);
            }

            var application = Context.Applications.FindByName(model.Name);
            if (application != null)
            {
                ModelState.AddModelError(Core.Infrastructure.Name.Of<AddViewModel, string>(x => x.Name), Strings.ApplicationNameInUseMessage);
                return PartialView(model);
            }

            application = model.ToApplication();
            Context.Applications.Save(application);

            Security.Refresh();
            return RedirectWithMessage(MVC.Account.ActionNames.List, Strings.ApplicationAddedMessage);
        }

        [HttpGet, CanManageApplications]
        public virtual ActionResult Edit(string id)
        {
            var application = Context.Applications.Get(id);
            if (application == null)
            {
                return RedirectToAction(MVC.Application.ActionNames.List);
            }

            return PartialView(EditViewModel.Create(application));
        }

        [HttpPost, CanManageApplications]
        public virtual ActionResult Edit(EditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(model);
            }

            var application = Context.Applications.FindByName(model.Name);
            if (application != null && application.Id != model.Id)
            {
                ModelState.AddModelError(Core.Infrastructure.Name.Of<AddViewModel, string>(x => x.Name), Strings.ApplicationNameInUseMessage);
                return PartialView(model);
            }

            application = Context.Applications.Get(model.Id);
            if (application == null)
            {
                return RedirectToAction(MVC.Application.ActionNames.List);
            }

            model.Update(application);

            Context.Applications.Save(application);

            Security.Refresh();
            return RedirectWithMessage(MVC.Account.ActionNames.List, Strings.ApplicationEditedMessage);
        }

        [HttpGet, CanManageApplications]
        public virtual ActionResult Delete(string id)
        {
            var application = Context.Applications.Get(id);
            if (application != null)
            {
                RedirectToAction(MVC.Application.ActionNames.List);
            }

            var users = Context.Users.FindWithPrivilegesTo(id);
            users.ForEach(x => x.Privileges.Remove(x.Privileges.Single(p => p.ApplicationId == id)));
            Context.Users.Save(users);

            Context.Applications.Delete(application);

            Security.Refresh();
            return RedirectWithMessage(MVC.Application.ActionNames.List, Strings.ApplicationDeletedMessage);
        }

        [HttpGet, CanManageApplications]
        public virtual ActionResult Menu()
        {
            var apps = Context.Applications.In(Security.ReadAccess)
                .OrderBy(x => x.Name)
                .Select(ListItemViewModel.Create)
                .ToList();
            return PartialView(apps);
        }

        private PartialViewResult List(ApplicationQuery query)
        {
            Session.Set(query);

            var applications = Context.Applications.Search(query);
            var model = ListViewModel.Create(query, applications);

            var message = GetRedirectedMessage();
            if (message != null)
            {
                model.Message = message;
            }

            return PartialView(MVC.Application.Views.List, model);
        }
    }
}