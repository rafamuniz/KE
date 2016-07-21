using KarmicEnergy.Web.Areas.Admin.ViewModels.Sync;

namespace KarmicEnergy.Web.Infrastructure.Mappings.Admin
{
    public class SyncProfile : AutoMapper.Profile
    {
        public SyncProfile()
        {
            #region ViewModel To Entity

            #endregion ViewModel To Entity

            #region Entity To ViewModel 

            this.CreateMap<Core.Entities.DataSync, ListViewModel>();

            #endregion Entity To ViewModel 
        }
    }
}