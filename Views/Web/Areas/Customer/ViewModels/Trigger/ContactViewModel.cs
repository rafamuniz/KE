using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Trigger
{
    public class ContactViewModel
    {
        #region Property
                
        public Guid Id { get; set; }
                
        public String Name { get; set; }

        public Boolean IsSelected { get; set; }
                
        #endregion Property

        #region Map

        public static List<ContactViewModel> Map(List<Core.Entities.Contact> entities)
        {
            List<ContactViewModel> vms = new List<ContactViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(ContactViewModel.Map(c)));
            }

            return vms;
        }

        public static ContactViewModel Map(Core.Entities.Contact entity)
        {            
            return Mapper.Map<Core.Entities.Contact, ContactViewModel>(entity);
        }

        #endregion Map
    }
}