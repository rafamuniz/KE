using KarmicEnergy.Core.Persistence;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System.Web;
using System.Web.Http;

namespace KarmicEnergy.Web.ApiControllers
{
    public abstract class ApiControllerBase : ApiController
    {
        #region Fields
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private KEUnitOfWork _KEUnitOfWork;
        #endregion Fields

        #region Properties
        protected KEUnitOfWork KEUnitOfWork
        {
            get
            {
                return _KEUnitOfWork ?? new KEUnitOfWork(false);
            }
            set
            {
                _KEUnitOfWork = value;
            }
        }
        protected IOwinContext Context
        {
            get
            {
                return HttpContext.Current.GetOwinContext();
            }
        }

        protected ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Context.GetUserManager<ApplicationUserManager>();
            }
            private set { _userManager = value; }
        }

        protected ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? Context.Get<ApplicationRoleManager>(); ;
            }
            private set { _roleManager = value; }
        }
        #endregion Fields
    }
}