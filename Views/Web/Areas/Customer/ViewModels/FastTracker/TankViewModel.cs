using System;
using System.Linq;
using System.Collections.Generic;
using KarmicEnergy.Core.Entities;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.FastTracker
{
    public class TankViewModel
    {
        #region Constructor
        public TankViewModel()
        {
            WaterVolumes = new List<WaterVolumeViewModel>();
            Alarms = new List<AlarmViewModel>();
        }
        #endregion Constructor

        #region Property

        #region Tank
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
                    return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "NoData"));

                //Int32 volume = 0;

                //if (Int32.TryParse(WaterVolumePercentage.ToString(), out volume))
                //{
                //    var rnd = Math.Round(WaterVolumePercentage.Value);
                //}

                var percentage = (Int32)WaterVolumePercentage;

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

        #endregion Tank

        #region Last Water Volume

        public Decimal? WaterVolumeLastValue { get; set; }
        public DateTime? WaterVolumeLastEventDate { get; set; }

        public Decimal? WaterVolumePercentage
        {
            get
            {
                if (WaterVolumeCapacity != 0 && WaterVolumeLastValue.HasValue)
                {
                    return (WaterVolumeLastValue / WaterVolumeCapacity) * 100;
                }
                else if (WaterVolumeCapacity == 0)
                {
                    return 0;
                }

                return null;
            }
        }
        #endregion Last Water Volume

        #region Last Temperature
        public Decimal? WaterTemperatureLastEventValue { get; set; }
        public DateTime? WaterTemperatureLastEventDate { get; set; }

        public Decimal? AmbientTemperatureLastEventValue { get; set; }
        public DateTime? AmbientTemperatureLastEventDate { get; set; }

        #endregion Last Temperature

        #region Water Volumes

        public List<WaterVolumeViewModel> WaterVolumes { get; set; }

        #endregion Water Volumes

        #region Alarms
        public List<AlarmViewModel> Alarms { get; set; }
        public Int32 TotalAlarms
        {
            get
            {
                if (Alarms != null)
                    return Alarms.Count;
                return 0;
            }
            private set { }
        }

        //tankViewModel.HasAlarmHigh = alarms.Where(x => x.Trigger.SeverityId == (Int16)SeverityEnum.Critical).Any();
        //tankViewModel.HasAlarmMedium = alarms.Where(x => x.Trigger.SeverityId == (Int16)SeverityEnum.Medium).Any();
        //tankViewModel.HasAlarmLow = alarms.Where(x => x.Trigger.SeverityId == (Int16)SeverityEnum.Low).Any();

        public Boolean HasAlarmCritical
        {
            get
            {
                if (Alarms != null && Alarms.Any())
                    return Alarms.Where(x => x.SeverityId == (Int16)SeverityEnum.Critical).Any();
                return false;
            }
            private set { }
        }

        public Boolean HasAlarmHigh
        {
            get
            {
                if (Alarms != null && Alarms.Any())
                    return Alarms.Where(x => x.SeverityId == (Int16)SeverityEnum.High).Any();
                return false;
            }
            private set { }
        }

        public Boolean HasAlarmMedium
        {
            get
            {
                if (Alarms != null && Alarms.Any())
                    return Alarms.Where(x => x.SeverityId == (Int16)SeverityEnum.Medium).Any();
                return false;
            }
            private set { }
        }

        public Boolean HasAlarmLow
        {
            get
            {
                if (Alarms != null && Alarms.Any())
                    return Alarms.Where(x => x.SeverityId == (Int16)SeverityEnum.Low).Any();
                return false;
            }
            private set { }
        }

        public Boolean HasAlarmInfo
        {
            get
            {
                if (Alarms != null && Alarms.Any())
                    return Alarms.Where(x => x.SeverityId == (Int16)SeverityEnum.Info).Any();
                return false;
            }
            private set { }
        }
        #endregion Alarms

        #endregion Property        
    }
}