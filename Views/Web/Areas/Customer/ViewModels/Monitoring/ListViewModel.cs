using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Monitoring
{
    public class ListViewModel
    {
        #region Constructor
        public ListViewModel()
        {

        }
        #endregion Constructor

        #region Property

        public Guid Id { get; set; }

        public Guid SiteId { get; set; }
        public String SiteName { get; set; }

        public Guid TankId { get; set; }
        public String TankName { get; set; }

        public Guid SensorId { get; set; }
        public String SensorName { get; set; }

        public Int32 ItemId { get; set; }
        public String ItemName { get; set; }

        public DateTime? AckDate { get; set; }
        public Guid? AckUserId { get; set; }
        public String AckUser { get; set; }

        public DateTime AlarmDate { get; set; }

        #endregion Property

        #region Map

        public static List<ListViewModel> Map(List<Core.Entities.Alarm> entities)
        {
            List<ListViewModel> vms = new List<ListViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(ListViewModel.Map(c)));
            }

            return vms;
        }

        public static ListViewModel Map(Core.Entities.Alarm entity)
        {
            Mapper.CreateMap<Core.Entities.Alarm, ListViewModel>();
            var viewmodel = Mapper.Map<Core.Entities.Alarm, ListViewModel>(entity);

            viewmodel.SiteId = entity.Trigger.SensorItem.Sensor.Tank.Site.Id;
            viewmodel.SiteName = entity.Trigger.SensorItem.Sensor.Tank.Site.Name;

            viewmodel.TankId = entity.Trigger.SensorItem.Sensor.Tank.Id;
            viewmodel.TankName = entity.Trigger.SensorItem.Sensor.Tank.Name;

            viewmodel.SensorId = entity.Trigger.SensorItem.Sensor.Id;
            viewmodel.SensorName = entity.Trigger.SensorItem.Sensor.Name;

            viewmodel.ItemId = entity.Trigger.SensorItem.Item.Id;
            viewmodel.ItemName = entity.Trigger.SensorItem.Item.Name;

            viewmodel.AlarmDate = entity.StartDate;
            viewmodel.AckDate = entity.LastAckDate;
            viewmodel.AckUserId = entity.LastAckUserId;

            return viewmodel;
        }

        #endregion Map
    }
}