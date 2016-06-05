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

        public Int16 SeverityId { get; set; }
        public String SeverityName { get; set; }

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
            var viewModel = Mapper.Map<Core.Entities.Alarm, ListViewModel>(entity);

            viewModel.SiteId = entity.Trigger.SensorItem.Sensor.Tank != null ? entity.Trigger.SensorItem.Sensor.Tank.Site.Id : entity.Trigger.SensorItem.Sensor.Site.Id;
            viewModel.SiteName = entity.Trigger.SensorItem.Sensor.Tank != null ? entity.Trigger.SensorItem.Sensor.Tank.Site.Name : entity.Trigger.SensorItem.Sensor.Site.Name;

            if (entity.Trigger.SensorItem.Sensor.Tank != null)
            {
                viewModel.TankId = entity.Trigger.SensorItem.Sensor.Tank.Id;
                viewModel.TankName = entity.Trigger.SensorItem.Sensor.Tank.Name;
            }

            viewModel.SensorId = entity.Trigger.SensorItem.Sensor.Id;
            viewModel.SensorName = entity.Trigger.SensorItem.Sensor.Name;

            viewModel.ItemId = entity.Trigger.SensorItem.Item.Id;
            viewModel.ItemName = entity.Trigger.SensorItem.Item.Name;

            viewModel.AlarmDate = entity.StartDate;
            viewModel.AckDate = entity.LastAckDate;
            viewModel.AckUserId = entity.LastAckUserId;

            viewModel.SeverityId = entity.Trigger.Severity.Id;
            viewModel.SeverityName = entity.Trigger.Severity.Name;

            return viewModel;
        }

        #endregion Map
    }
}