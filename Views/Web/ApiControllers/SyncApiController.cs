using KarmicEnergy.Core.Entities;
using KarmicEnergy.Core.Persistence;
using KarmicEnergy.Web.Entities;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace KarmicEnergy.Web.ApiControllers
{
    public class SyncApiController : ApiControllerBase
    {
        //#region Tank
        //[HttpGet]
        //[Route("api/v1/json/customer/tank/gets/{customerId}", Name = "GetsTankByCustomerId")]
        //public IHttpActionResult GetsTankByCustomerId(String customerId)
        //{
        //    List<Tank> tanks = KEUnitOfWork.TankRepository.GetsByCustomerId(Guid.Parse(customerId));
        //    return Ok(tanks);
        //}
        //#endregion Tank

        #region User

        [HttpGet]
        [Route("sync/users/{siteId}", Name = "SyncUsers")]
        public IHttpActionResult SyncUsers(String siteId, List<ApplicationUser> users)
        {
            try
            {
                foreach (var user in users)
                {
                    //UserManager.RemoveFromRolesAsync(user.Id, user.Roles.)
                    //user.Roles.Remove();
                    foreach (var role in user.Roles)
                    {
                        //  UserManager.IsInRoleAsync();

                        //foreach (var role in user.Roles)
                        //{
                            
                        //}
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion User

        #region Address

        [HttpGet]
        [Route("sync/addresses/{siteId}", Name = "SyncAddressess")]
        public IHttpActionResult SyncAddressess(String siteId, List<Address> addresses)
        {
            try
            {
                KEUnitOfWork.AddressRepository.AddOrUpdateRange(addresses);
                KEUnitOfWork.Complete();
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        #endregion Address

        #region Tank
        #endregion Tank

    }
}