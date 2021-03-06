﻿using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Customer.ViewModels.Map;
using KarmicEnergy.Web.Areas.Customer.ViewModels.Site;

namespace KarmicEnergy.Web.Infrastructure.Mappings.Customer
{
    public class MapProfile : AutoMapper.Profile
    {
        public MapProfile()
        {
            #region ViewModel To Entity

            this.CreateMap<IndexViewModel, Core.Entities.Site>();

            #endregion ViewModel To Entity

            #region Entity To ViewModel 

            this.CreateMap<Core.Entities.Site, IndexViewModel>()
                .ForMember(x => x.Sites, opt => opt.Ignore());

            this.CreateMap<Core.Entities.Site, Areas.Customer.ViewModels.Map.SiteViewModel>();
            this.CreateMap<Core.Entities.Pond, Areas.Customer.ViewModels.Map.PondViewModel>();
            this.CreateMap<Core.Entities.Tank, Areas.Customer.ViewModels.Map.TankViewModel>()
                .ForMember(x => x.TankModelId, opt => opt.MapFrom(src => src.TankModelId))
                .ForMember(x => x.TankModelImage, opt => opt.MapFrom(src => src.TankModel.ImageFilename));
            this.CreateMap<Core.Entities.Sensor, Areas.Customer.ViewModels.Map.SensorViewModel>();

            this.CreateMap<Core.Entities.Alarm, Areas.Customer.ViewModels.Map.AlarmViewModel>();


            #endregion Entity To ViewModel 
        }
    }
}