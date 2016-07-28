using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Munizoft.Extensions;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Monitoring
{
    public class EventDetailViewModel
    {
        #region Constructor
        public EventDetailViewModel()
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

        [Display(Name = "Value")]
        public String Value { get; set; }

        [Display(Name = "Value Unit")]
        public String ValueWithUnit { get; set; }

        [Display(Name = "Date")]
        public DateTime EventDate { get; set; }

        [Display(Name = "Date")]
        public DateTime EventDateLocal
        {
            get { return EventDate.ToLocalTime(); }
            set { }
        }

        #endregion Property

        #region Map

        public static List<EventDetailViewModel> Map(IEnumerable<Core.Entities.SensorItemEvent> entities)
        {
            List<EventDetailViewModel> vms = new List<EventDetailViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(EventDetailViewModel.MapEntityToVM(c)));
            }

            return vms;
        }

        public void Map(Core.Entities.SensorItemEvent entity)
        {
            Mapper.Map<Core.Entities.SensorItemEvent, EventDetailViewModel>(entity);

            if (entity.SensorItem.Sensor.Site != null)
            {
                this.SiteId = entity.SensorItem.Sensor.Site.Id;
                this.SiteName = entity.SensorItem.Sensor.Site.Name;
            }

            if (entity.SensorItem.Sensor.Pond != null)
            {
                this.SiteId = entity.SensorItem.Sensor.Pond.Site.Id;
                this.SiteName = entity.SensorItem.Sensor.Pond.Site.Name;

                this.PondId = entity.SensorItem.Sensor.Pond.Id;
                this.PondName = entity.SensorItem.Sensor.Pond.Name;
            }

            if (entity.SensorItem.Sensor.Tank != null)
            {
                this.SiteId = entity.SensorItem.Sensor.Tank.Site.Id;
                this.SiteName = entity.SensorItem.Sensor.Tank.Site.Name;

                this.TankId = entity.SensorItem.Sensor.Tank.Id;
                this.TankName = entity.SensorItem.Sensor.Tank.Name;
            }

            this.SensorId = entity.SensorItem.Sensor.Id;
            this.SensorName = entity.SensorItem.Sensor.Name;

            this.ItemId = entity.SensorItem.Item.Id;
            this.ItemName = entity.SensorItem.Item.Name;

            this.EventDate = entity.EventDate;

            this.Value = entity.ConverterItemUnit();
            this.ValueWithUnit = String.Format("{0} {1}", entity.ConverterItemUnit(), entity.SensorItem.Unit.Symbol);
        }

        public static EventDetailViewModel MapEntityToVM(Core.Entities.SensorItemEvent entity)
        {
            var viewModel = Mapper.Map<Core.Entities.SensorItemEvent, EventDetailViewModel>(entity);

            if (entity.SensorItem.Sensor.Site != null)
            {
                viewModel.SiteId = entity.SensorItem.Sensor.Site.Id;
                viewModel.SiteName = entity.SensorItem.Sensor.Site.Name;
            }

            if (entity.SensorItem.Sensor.Pond != null)
            {
                viewModel.SiteId = entity.SensorItem.Sensor.Pond.Site.Id;
                viewModel.SiteName = entity.SensorItem.Sensor.Pond.Site.Name;

                viewModel.PondId = entity.SensorItem.Sensor.Pond.Id;
                viewModel.PondName = entity.SensorItem.Sensor.Pond.Name;
            }

            if (entity.SensorItem.Sensor.Tank != null)
            {
                viewModel.SiteId = entity.SensorItem.Sensor.Tank.Site.Id;
                viewModel.SiteName = entity.SensorItem.Sensor.Tank.Site.Name;

                viewModel.TankId = entity.SensorItem.Sensor.Tank.Id;
                viewModel.TankName = entity.SensorItem.Sensor.Tank.Name;
            }

            viewModel.SensorId = entity.SensorItem.Sensor.Id;
            viewModel.SensorName = entity.SensorItem.Sensor.Name;

            viewModel.ItemId = entity.SensorItem.Item.Id;
            viewModel.ItemName = entity.SensorItem.Item.Name;

            viewModel.EventDate = entity.EventDate;

            viewModel.Value = entity.ConverterItemUnit();
            viewModel.ValueWithUnit = String.Format("{0} {1}", entity.ConverterItemUnit(), entity.SensorItem.Unit.Symbol);

            return viewModel;
        }
        #endregion Map
    }
}