using ByteCarrot.Masslog.Core.Infrastructure.Extensions;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ByteCarrot.Masslog.Core.DomainModel.Entities;
using ByteCarrot.Masslog.Core.DomainModel.Repositories;
using ByteCarrot.Masslog.Web.Infrastructure;
using ByteCarrot.Masslog.Web.Infrastructure.Security;

namespace ByteCarrot.Masslog.Web.Controllers.Account
{
    public partial class AccountController : MvcController
    {
        private readonly IAddUserViewModelAssembler _addAssembler;
        private readonly IEditUserViewModelAssembler _editAssembler;

        public AccountController(IAddUserViewModelAssembler addAssembler, IEditUserViewModelAssembler editAssembler)
        {
            _addAssembler = addAssembler;
            _editAssembler = editAssembler;
        }

        [HttpGet, AppendLayoutViewModel]
        public virtual ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public virtual ActionResult SignIn()
        {
            var model = new SignInViewModel();
            return PartialView(model);
        }

        [HttpPost]
        public virtual ActionResult SignIn(SignInViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(model);
            }

            var user = Context.Users.FindByCredentials(model.Username, model.Password);
            if (user == null)
            {
                model.Message = Strings.InvalidUsernamePasswordMessage;
                return PartialView(model);
            }

            if (!user.IsAdministrator && user.Privileges.Count == 0)
            {
                model.Message = Strings.NotAuthorizedToAccessLogsMessage;
                return PartialView(model);
            }

            var expiration = model.RememberMe ? DateTime.Now.AddYears(10) : DateTime.Now.AddMinutes(30);
            var roles = user.IsAdministrator ? Role.Administrator : String.Empty;
            var ticket = new FormsAuthenticationTicket(1, user.Username, DateTime.Now, expiration, model.RememberMe, roles);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            if (model.RememberMe)
            {
                cookie.Expires = ticket.Expiration;
            }
            Response.Cookies.Add(cookie);

            return PartialView(MVC.Account.Views.Redirect);
        }

        [HttpGet, Authorize]
        public virtual ActionResult SignOut()
        {
            Session.Destroy();
            FormsAuthentication.SignOut();
            return RedirectToAction(MVC.Account.Index());
        }

        // Settings

        [HttpGet, CanManageUsers]
        public virtual PartialViewResult List()
        {
            return List(new UserQuery
                            {
                                PageNumber = 1,
                                PageSize = 10,
                                SortColumn = Core.Infrastructure.Name.Of<User, string>(x => x.Username),
                                SortDirection = SortDirection.Ascending
                            });
        }

        [HttpGet, CanManageUsers]
        public virtual PartialViewResult Page(int page)
        {
            var query = Session.Get<UserQuery>();
            query.PageNumber = page;

            return List(query);
        }

        [HttpGet, CanManageUsers]
        public virtual PartialViewResult Sort(string column, string direction)
        {
            var query = Session.Get<UserQuery>();
            query.SortColumn = column;
            query.SortDirection = (SortDirection)Enum.Parse(typeof(SortDirection), direction);

            return List(query);
        }

        private PartialViewResult List(UserQuery query)
        {
            Session.Set(query);

            var users = Context.Users.Search(query);
            var model = UsersViewModel.Create(query, users);
            model.CurrentUsername = Security.Username;

            var message = GetRedirectedMessage();
            if (message != null)
            {
                model.Message = message;
            }

            return PartialView(MVC.Account.Views.List, model);
        }

        // Add

        [HttpGet, CanManageUsers]
        public virtual ActionResult Add()
        {
            return PartialView(_addAssembler.ToViewModel());
        }

        [HttpPost, CanManageUsers]
        public virtual ActionResult Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(model);
            }

            var user = Context.Users.FindByUsername(model.Username);
            if (user != null)
            {
                ModelState.AddModelError(Core.Infrastructure.Name.Of<AddViewModel, string>(x => x.Username), Strings.UsernameInUseMessage);
                return PartialView(model);
            }

            Context.Users.Save(_addAssembler.ToEntity(model));

            return RedirectWithMessage(MVC.Account.ActionNames.List, Strings.UserAddedMessage);
        }

        // Edit
        [HttpGet, CanManageUsers]
        public virtual ActionResult Edit(string id)
        {
            var user = Context.Users.Get(id);
            if (user == null)
            {
                return RedirectToAction(MVC.Account.ActionNames.List);
            }

            return PartialView(_editAssembler.ToViewModel(user));
        }

        [HttpPost, CanManageUsers]
        public virtual ActionResult Edit(EditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(model);
            }

            var user = Context.Users.Get(model.Id);
            if (user == null)
            {
                return RedirectToAction(MVC.Account.ActionNames.List);
            }

            _editAssembler.Update(model, user);
            Context.Users.Save(user);

            return RedirectWithMessage(MVC.Account.ActionNames.List, Strings.UserEditedMessage);
        }

        // Delete

        [HttpGet, CanManageUsers]
        public virtual ActionResult Delete(string id)
        {
            var user = Context.Users.Get(id);
            if (user != null)
            {
                RedirectToAction(MVC.Account.ActionNames.List);
            }
            Context.Users.Delete(user);
            return RedirectWithMessage(MVC.Account.ActionNames.List, Strings.UserDeletedMessage);
        }

        // Change Password

        [HttpGet, CanChangePassword]
        public virtual ActionResult ChangePassword()
        {
            return PartialView(new ChangePasswordViewModel { Username = Security.Username });
        }

        [HttpPost, CanChangePassword]
        public virtual ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(model);
            }

            var user = Context.Users.FindByCredentials(Security.Username, model.OldPassword);
            if (user == null)
            {
                ModelState.AddModelError(Core.Infrastructure.Name.Of<ChangePasswordViewModel, string>(x => x.OldPassword), Strings.InvalidPasswordMessage);
                return PartialView(model);
            }

            user.Password = model.NewPassword.Md5Hash();
            Context.Users.Save(user);

            return RedirectToAction(MVC.Activities.Index());
        }
    }
}