using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Tank
{
    public class DashboardTankViewModel
    {
        #region Property

        [Display(Name = "SiteId")]
        [Required]
        public Guid SiteId { get; set; }

        public Guid TankId { get; set; }

        [Display(Name = "Name")]
        public String TankName { get; set; }

        public Int32 TankModelId { get; set; }
        public String TankModelImage { get; set; }

        public String UrlImageTankModel
        {
            get
            {
                var baseurl = "/images/tankmodels/";

                if (!WaterVolumePercentage.HasValue)
                    return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "NoData"));

                Int32 volume = 0;

                if (Int32.TryParse(WaterVolumePercentage.ToString(), out volume))
                {
                    var rnd = Math.Round(WaterVolumePercentage.Value);
                }

                var percentage = WaterVolumePercentage * 100;

                if (percentage == 0)
                {
                    return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "0"));
                }
                else if (percentage > 0 && percentage < 20)
                {
                    return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "10"));
                }
                else if (percentage >= 20 && percentage < 30)
                {
                    return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "20"));
                }
                else if (percentage >= 30 && percentage < 40)
                {
                    return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "30"));
                }
                else if (percentage >= 40 && percentage < 50)
                {
                    return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "40"));
                }
                else if (percentage >= 50 && percentage < 60)
                {
                    return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "50"));
                }
                else if (percentage >= 60 && percentage < 70)
                {
                    return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "60"));
                }
                else if (percentage >= 70 && percentage < 80)
                {
                    return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "70"));
                }
                else if (percentage >= 80 && percentage < 90)
                {
                    return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "80"));
                }
                else if (percentage >= 90 && percentage < 100)
                {
                    return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "90"));
                }
                else if (percentage == 100)
                {
                    return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "100"));
                }

                return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "NoData"));
            }
            private set { }
        }

        public Decimal WaterVolumeCapacity { get; set; }

        public Decimal? WaterVolumePercentage
        {
            get
            {
                if (WaterVolume.HasValue)
                {
                    return WaterVolume / WaterVolumeCapacity;
                }
                return null;
            }
            set { }
        }

        public Decimal WaterVolumeRemaining
        {
            get { return WaterVolumeCapacity - WaterVolume.Value; }
            set { }
        }

        public Decimal? WaterVolume { get; set; }
        public DateTime? WaterVolumeLastMeasurement { get; set; }

        public Decimal AmbientTemperature { get; set; }
        public DateTime? AmbientTemperatureLastMeasurement { get; set; }

        public Decimal? WaterTemperature { get; set; }
        public DateTime? WaterTemperatureLastMeasurement { get; set; }

        public Int32? Alarms { get; set; }

        public String Color { get; set; }

        #endregion Property

        #region Map

        public static List<DashboardTankViewModel> Map(List<Core.Entities.Tank> entities)
        {
            List<DashboardTankViewModel> vms = new List<DashboardTankViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(DashboardTankViewModel.Map(c)));
            }

            return vms;
        }

        public static DashboardTankViewModel Map(Core.Entities.Tank entity)
        {
            Mapper.CreateMap<Core.Entities.Tank, DashboardTankViewModel>();
            return Mapper.Map<Core.Entities.Tank, DashboardTankViewModel>(entity);
        }

        #endregion Map
    }
}