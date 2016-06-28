using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Monitoring
{
    public class AlarmDetailViewModel
    {
        #region Constructor
        public AlarmDetailViewModel()
        {

        }
        #endregion Constructor

        #region Property

        public Guid Id { get; set; }

        [Display(Name = "Site")]
        public Guid SiteId { get; set; }
        [Display(Name = "Site")]
        public String SiteName { get; set; }

        [Display(Name = "Pond")]
        public Guid? PondId { get; set; }
        [Display(Name = "Pond")]
        public String PondName { get; set; }

        [Display(Name = "Tank")]
        public Guid? TankId { get; set; }
        [Display(Name = "Tank")]
        public String TankName { get; set; }

        [Display(Name = "Sensor")]
        public Guid SensorId { get; set; }
        [Display(Name = "Sensor")]
        public String SensorName { get; set; }

        [Display(Name = "Item")]
        public Int32 ItemId { get; set; }
        [Display(Name = "Item")]
        public String ItemName { get; set; }

        [Display(Name = "Trigger")]
        public Guid TriggerId { get; set; }

        [Display(Name = "Ack Last Date")]
        public DateTime? AckLastDate { get; set; }
        [Display(Name = "Ack Last Date")]
        public DateTime? AckLastDateLocal
        {
            get
            {
                if (AckLastDate.HasValue)
                    return AckLastDate.Value.ToLocalTime();
                return null;
            }
            set { }
        }

        [Display(Name = "Last User")]
        public Guid? AckLastUserId { get; set; }
        [Display(Name = "Last User")]
        public String AckLastUsername { get; set; }

        [Display(Name = "Alarm Date")]
        public DateTime AlarmDate { get; set; }
        [Display(Name = "Alarm Date")]
        public DateTime AlarmDateLocal
        {
            get { return AlarmDate.ToLocalTime(); }
            set { }
        }

        [Display(Name = "Severity")]
        public Int16 SeverityId { get; set; }
        [Display(Name = "Severity")]
        public String SeverityName { get; set; }

        [Display(Name = "Value")]
        public String Value { get; set; }

        #endregion Property

        #region Map

        public static List<AlarmDetailViewModel> Map(List<Core.Entities.Alarm> entities)
        {
            List<AlarmDetailViewModel> vms = new List<AlarmDetailViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(AlarmDetailViewModel.Map(c)));
            }

            return vms;
        }

        public static AlarmDetailViewModel Map(Core.Entities.Alarm entity)
        {
            Mapper.CreateMap<Core.Entities.Alarm, AlarmDetailViewModel>();
            var viewModel = Mapper.Map<Core.Entities.Alarm, AlarmDetailViewModel>(entity);
                        
            viewModel.SiteId = entity.Trigger.SensorItem.Sensor.Tank != null ? entity.Trigger.SensorItem.Sensor.Tank.Site.Id : entity.Trigger.SensorItem.Sensor.Site.Id;
            viewModel.SiteName = entity.Trigger.SensorItem.Sensor.Tank != null ? entity.Trigger.SensorItem.Sensor.Tank.Site.Name : entity.Trigger.SensorItem.Sensor.Site.Name;

            if (entity.Trigger.SensorItem.Sensor.Pond != null)
            {
                viewModel.PondId = entity.Trigger.SensorItem.Sensor.Pond.Id;
                viewModel.PondName = entity.Trigger.SensorItem.Sensor.Pond.Name;
            }

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
            viewModel.AckLastDate = entity.LastAckDate;
            viewModel.AckLastUserId = entity.LastAckUserId;
            viewModel.AckLastUsername = entity.LastAckUserName;

            viewModel.SeverityId = entity.Trigger.Severity.Id;
            viewModel.SeverityName = entity.Trigger.Severity.Name;

            return viewModel;
        }

        #endregion Map
    }
}