using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Trigger
{
    public class ListViewModel
    {
        #region Property

        public Guid Id { get; set; }

        [Display(Name = "Status")]
        [Required]
        public String Status { get; set; }

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

        [Display(Name = "Operator")]
        public String Operator { get; set; }

        [Display(Name = "Value")]
        public String Value { get; set; }

        [Display(Name = "Expression")]
        public String Expression
        {
            get
            {
                return String.Format("{0} {1}", Operator, Value);
            }
            private set { }
        }

        #endregion Property

        #region Map

        public static List<ListViewModel> Map(List<Core.Entities.Trigger> entities)
        {
            List<ListViewModel> vms = new List<ListViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(ListViewModel.Map(c)));
            }

            return vms;
        }

        public static ListViewModel Map(Core.Entities.Trigger entity)
        {
            Mapper.CreateMap<Core.Entities.Trigger, ListViewModel>();
            var viewModel = Mapper.Map<Core.Entities.Trigger, ListViewModel>(entity);
            viewModel.SiteId = entity.SensorItem.Sensor.Tank != null ? entity.SensorItem.Sensor.Tank.Site.Id : entity.SensorItem.Sensor.Site.Id;
            viewModel.SiteName = entity.SensorItem.Sensor.Tank != null ? entity.SensorItem.Sensor.Tank.Site.Name : entity.SensorItem.Sensor.Site.Name;

            viewModel.TankId = entity.SensorItem.Sensor.Tank != null ? entity.SensorItem.Sensor.Tank.Id : (Guid?)null;
            viewModel.TankName = entity.SensorItem.Sensor.Tank != null ? entity.SensorItem.Sensor.Tank.Name : String.Empty;

            viewModel.SensorId = entity.SensorItem.Sensor.Id;
            viewModel.SensorName = entity.SensorItem.Sensor.Name;

            viewModel.ItemId = entity.SensorItem.Item.Id;
            viewModel.ItemName = entity.SensorItem.Item.Name;

            viewModel.Operator = entity.Operator.Symbol;
            viewModel.Value = entity.Value;

            return viewModel;
        }

        #endregion Map
    }
}