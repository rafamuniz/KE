using KarmicEnergy.Web.Areas.Admin.ViewModels.Log;

namespace KarmicEnergy.Web.Infrastructure.Mappings.Admin
{
    public class LogProfile : AutoMapper.Profile
    {
        public LogProfile()
        {
            #region ViewModel To Entity

            this.CreateMap<ListViewModel, Core.Entities.Log>();

            #endregion ViewModel To Entity

            #region Entity To ViewModel 

            this.CreateMap<Core.Entities.Log, ListViewModel>();

            #endregion Entity To ViewModel 
        }
    }
}