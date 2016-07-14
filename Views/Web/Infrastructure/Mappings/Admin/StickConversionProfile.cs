using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Admin.ViewModels.StickConversion;

namespace KarmicEnergy.Web.Infrastructure.Mappings.Admin
{
    public class StickConversionProfile : AutoMapper.Profile
    {
        public StickConversionProfile()
        {
            #region ViewModel To Entity
                       

            #endregion ViewModel To Entity

            #region Entity To ViewModel 

            this.CreateMap<Core.Entities.StickConversion, ListViewModel>();
            this.CreateMap<Core.Entities.StickConversion, CreateViewModel>();
            this.CreateMap<Core.Entities.StickConversion, EditViewModel>();

            #endregion Entity To ViewModel 
        }
    }
}