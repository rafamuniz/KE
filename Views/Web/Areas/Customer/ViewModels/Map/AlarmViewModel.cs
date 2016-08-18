using AutoMapper;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Map
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

        public DateTime AlarmDateLocal
        {
            get { return AlarmDate.ToLocalTime(); }
            private set { }
        }

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

            viewModel.AlarmValue = entity.Value;
            viewModel.AlarmDate = entity.StartDate;

            if (entity.Trigger != null)
            {
                viewModel.TriggerId = entity.TriggerId;

                if (entity.Trigger.Severity != null)
                {
                    viewModel.SeverityId = entity.Trigger.Severity.Id;
                    viewModel.SeverityName = entity.Trigger.Severity.Name;
                }

                if (entity.Trigger.SensorItem != null)
                {
                    if (entity.Trigger.SensorItem.Item != null)
                    {
                        viewModel.ItemId = entity.Trigger.SensorItem.Item.Id;
                        viewModel.ItemName = entity.Trigger.SensorItem.Item.Name;
                    }

                    if (entity.Trigger.SensorItem.Sensor != null)
                    {
                        viewModel.SensorId = entity.Trigger.SensorItem.Sensor.Id;
                        viewModel.SensorName = entity.Trigger.SensorItem.Sensor.Name;

                        // Pond 
                        if (entity.Trigger.SensorItem.Sensor.Pond != null)
                        {
                            if (entity.Trigger.SensorItem.Sensor.Pond.Site != null)
                            {
                                viewModel.SiteId = entity.Trigger.SensorItem.Sensor.Pond.Site.Id;
                                viewModel.SiteName = entity.Trigger.SensorItem.Sensor.Pond.Site.Name;
                            }

                            viewModel.TankId = entity.Trigger.SensorItem.Sensor.Pond.Id;
                            viewModel.TankName = entity.Trigger.SensorItem.Sensor.Pond.Name;
                        }

                        // Tank
                        if (entity.Trigger.SensorItem.Sensor.Tank != null)
                        {
                            if (entity.Trigger.SensorItem.Sensor.Tank.Site != null)
                            {
                                viewModel.SiteId = entity.Trigger.SensorItem.Sensor.Tank.Site.Id;
                                viewModel.SiteName = entity.Trigger.SensorItem.Sensor.Tank.Site.Name;
                            }

                            viewModel.TankId = entity.Trigger.SensorItem.Sensor.Tank.Id;
                            viewModel.TankName = entity.Trigger.SensorItem.Sensor.Tank.Name;
                        }
                    }
                }
            }

            return viewModel;
        }

        #endregion Map      
    }
}