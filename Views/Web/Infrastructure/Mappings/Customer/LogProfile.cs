using KarmicEnergy.Web.Areas.Customer.ViewModels.Log;

namespace KarmicEnergy.Web.Infrastructure.Mappings.Customer
{
    public class LogProfile : AutoMapper.Profile
    {
        public LogProfile()
        {
            #region ViewModel To Entity


            #endregion ViewModel To Entity

            #region Entity To ViewModel 

            this.CreateMap<Core.Entities.Log, ListViewModel>();
            
            #endregion Entity To ViewModel 
        }
    }
}