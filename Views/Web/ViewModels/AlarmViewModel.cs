using AutoMapper;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KarmicEnergy.Web.ViewModels
{
    public class AlarmViewModel
    {
        #region Constructor
        public AlarmViewModel()
        {

        }
        #endregion Constructor

        #region Property

        public Guid Id { get; set; }

        public Guid? SiteId { get; set; }
        public String SiteName { get; set; }

        public Guid? TankId { get; set; }
        public String TankName { get; set; }

        public Guid? SensorId { get; set; }
        public String SensorName { get; set; }

        public Int32 ItemId { get; set; }
        public String ItemName { get; set; }

        public Guid TriggerId { get; set; }

        public Int16 SeverityId { get; set; }
        public String SeverityName { get; set; }

        public String AlarmValue { get; set; }
        public DateTime AlarmDate { get; set; }

        #endregion Property

        #region Map

        public static List<AlarmViewModel> Map(List<Core.Entities.Alarm> entities)
        {
            List<AlarmViewModel> vms = new List<AlarmViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(AlarmViewModel.Map(c)));
            }

            return vms;
        }

        public static AlarmViewModel Map(Core.Entities.Alarm entity)
        {     
            var viewModel = Mapper.Map<Core.Entities.Alarm, AlarmViewModel>(entity);

            viewModel.ItemId = entity.Trigger.SensorItem.Item.Id;
            viewModel.ItemName = entity.Trigger.SensorItem.Item.Name;
            viewModel.TriggerId = entity.TriggerId;

            viewModel.SeverityId = entity.Trigger.Severity.Id;
            viewModel.SeverityName = entity.Trigger.Severity.Name;

            viewModel.SensorId = entity.Trigger.SensorItem.Sensor.Id;
            viewModel.SensorName = entity.Trigger.SensorItem.Sensor.Name;

            viewModel.SiteId = entity.Trigger.SensorItem.Sensor.Tank != null ? entity.Trigger.SensorItem.Sensor.Tank.Site.Id : entity.Trigger.SensorItem.Sensor.Site.Id;
            viewModel.SiteName = entity.Trigger.SensorItem.Sensor.Tank != null ? entity.Trigger.SensorItem.Sensor.Tank.Site.Name : entity.Trigger.SensorItem.Sensor.Site.Name;

            if (entity.Trigger.SensorItem.Sensor.Tank != null)
            {
                viewModel.TankId = entity.Trigger.SensorItem.Sensor.Tank.Id;
                viewModel.TankName = entity.Trigger.SensorItem.Sensor.Tank.Name;
            }

            return viewModel;
        }

        #endregion Map      

    }
}