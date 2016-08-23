namespace KarmicEnergy.Web.Infrastructure.Mappings.Customer
{
    public class HomeProfile : AutoMapper.Profile
    {
        public HomeProfile()
        {
            #region ViewModel To Entity

            #endregion ViewModel To Entity

            #region Entity To ViewModel 
            this.CreateMap<Core.Entities.Alarm, KarmicEnergy.Web.ViewModels.AlarmViewModel>();
            #endregion Entity To ViewModel 
        }

    }
}