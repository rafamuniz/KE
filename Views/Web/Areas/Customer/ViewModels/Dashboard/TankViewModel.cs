using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Dashboard
{
    public class TankViewModel
    {
        #region Constructor

        public TankViewModel()
        {
            WaterVolumes = new List<WaterVolumeViewModel>();
        }

        #endregion Constructor

        #region Property

        public Guid SiteId { get; set; }

        public Guid Id { get; set; }

        public String Name { get; set; }

        public Int32 TankModelId { get; set; }
        public String TankModelImage { get; set; }

        public String UrlImageTankModel
        {
            get
            {
                var baseurl = "/images/tankmodels/";

                if (!WaterVolumePercentage.HasValue)
                    return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage == null ? String.Empty : TankModelImage.Replace("{info}", "NoData"));

                var percentage = (Int16)WaterVolumePercentage;

                if (percentage <= 0)
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
                else if (percentage >= 100)
                {
                    return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "100"));
                }

                return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "NoData"));
            }
            private set { }
        }

        public Decimal WaterVolumeCapacity { get; set; }

        public Decimal? WaterVolumeLastValue { get; set; }

        public Decimal? WaterVolumePercentage
        {
            get
            {
                if (WaterVolumeLastValue.HasValue)
                {
                    return (WaterVolumeLastValue.Value / WaterVolumeCapacity) * 100;
                }

                return null;
            }
            private set { }
        }

        public DateTime? WaterVolumeLastEventDate { get; set; }

        public Decimal? WaterTemperatureLastEventValue { get; set; }
        public DateTime? WaterTemperatureLastEventDate { get; set; }

        public Decimal? AmbientTemperatureLastEventValue { get; set; }
        public DateTime? AmbientTemperatureLastEventDate { get; set; }

        public Int32? Alarms { get; set; }

        public Boolean HasAlarmHigh { get; set; }
        public Boolean HasAlarmMedium { get; set; }
        public Boolean HasAlarmLow { get; set; }

        //tankViewModel.HasAlarmHigh = alarms.Where(x => x.Trigger.SeverityId == (Int16)SeverityEnum.Critical).Any();
        //tankViewModel.HasAlarmMedium = alarms.Where(x => x.Trigger.SeverityId == (Int16)SeverityEnum.Medium).Any();
        //tankViewModel.HasAlarmLow = alarms.Where(x => x.Trigger.SeverityId == (Int16)SeverityEnum.Low).Any();

        public List<WaterVolumeViewModel> WaterVolumes { get; set; }

        #endregion Property

        #region Map

        public static List<TankViewModel> Map(List<Core.Entities.Tank> entities)
        {
            List<TankViewModel> vms = new List<TankViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(TankViewModel.Map(c)));
            }

            return vms;
        }

        public static TankViewModel Map(Core.Entities.Tank entity)
        {

            return Mapper.Map<Core.Entities.Tank, TankViewModel>(entity);
        }

        #endregion Map
    }
}