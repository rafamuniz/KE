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

        public Guid? PondId { get; set; }
        public String PondName { get; set; }

        public Guid? TankId { get; set; }
        public String TankName { get; set; }

        public Guid SensorId { get; set; }
        public String SensorName { get; set; }

        public Int32 ItemId { get; set; }
        public String ItemName { get; set; }

        public DateTime EventDate { get; set; }

        public DateTime EventDateLocal
        {
            get { return EventDate.ToLocalTime(); }
            set { }
        }

        public String Value { get; set; }
        #endregion Property

        #region Map

        public static List<ListViewModel> Map(List<Core.Entities.SensorItemEvent> entities)
        {
            List<ListViewModel> vms = new List<ListViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(ListViewModel.Map(c)));
            }

            return vms;
        }

        public static ListViewModel Map(Core.Entities.SensorItemEvent entity)
        {            
            var viewModel = Mapper.Map<Core.Entities.SensorItemEvent, ListViewModel>(entity);

            viewModel.Id = entity.Id;

            if (entity.SensorItem.Sensor.Site != null)
            {
                viewModel.SiteId = entity.SensorItem.Sensor.Site.Id;
                viewModel.SiteName = entity.SensorItem.Sensor.Site.Name;
            }
            else if (entity.SensorItem.Sensor.Pond != null)
            {
                viewModel.SiteId = entity.SensorItem.Sensor.Pond.Site.Id;
                viewModel.SiteName = entity.SensorItem.Sensor.Pond.Site.Name;

                viewModel.PondId = entity.SensorItem.Sensor.Pond.Id;
                viewModel.PondName = entity.SensorItem.Sensor.Pond.Name;
            }
            else if (entity.SensorItem.Sensor.Tank != null)
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

            viewModel.Value = String.Format("{0} {1}", entity.ConverterItemUnit(), entity.SensorItem.Unit.Symbol);

            return viewModel;
        }

        #endregion Map
    }
}