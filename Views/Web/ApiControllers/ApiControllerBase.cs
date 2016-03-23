using KarmicEnergy.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;


namespace KarmicEnergy.Web.ApiControllers
{
    public abstract class ApiControllerBase : ApiController
    {
        private KEUnitOfWork _KEUnitOfWork;

        protected KEUnitOfWork KEUnitOfWork
        {
            get
            {
                return _KEUnitOfWork ?? new KEUnitOfWork();
            }
            set
            {
                _KEUnitOfWork = value;
            }
        }
    }
}