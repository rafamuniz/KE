﻿using KarmicEnergy.Core.Entities;
using KarmicEnergy.Web.Areas.Customer.ViewModels.Contact;

namespace KarmicEnergy.Web.Infrastructure.Mappings.Customer
{
    public class DashboardProfile : AutoMapper.Profile
    {
        public DashboardProfile()
        {
            #region ViewModel To Entity

            this.CreateMap<ListViewModel, Core.Entities.Contact>();
            this.CreateMap<CreateViewModel, Core.Entities.Contact>().ForMember(x => x.Address, opt => opt.Ignore());
            this.CreateMap<EditViewModel, Core.Entities.Contact>().ForMember(x => x.Address, opt => opt.Ignore());

            #endregion ViewModel To Entity

            #region Entity To ViewModel 

            this.CreateMap<Core.Entities.Contact, EditViewModel>().ForMember(x => x.Address, opt => opt.Ignore());

            #endregion Entity To ViewModel 
        }
    }
}