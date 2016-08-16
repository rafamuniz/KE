using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using KarmicEnergy.Web.ViewModels;
using AutoMapper;
using Munizoft.Extensions;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Map
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

        public String Latitude { get; set; }
        public String Longitude { get; set; }

        public Int32 TankModelId { get; set; }
        public String TankModelImage { get; set; }

        public String UrlImageTankModel
        {
            get
            {
                if (TankModelImage != null && TankModelId != default(Int32))
                {
                    var baseurl = "/images/tankmodels/";

                    if (!WaterVolumePercentage.HasValue)
                        return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "nodata").Replace("-{color}", ""));

                    if (WaterVolumePercentage <= 0)
                    {
                        return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "empty").Replace("-{color}", ""));
                    }
                    else if (WaterVolumePercentage > 0 && WaterVolumePercentage < 20)
                    {
                        return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "10").Replace("{color}", TankColor));
                    }
                    else if (WaterVolumePercentage >= 20 && WaterVolumePercentage < 30)
                    {
                        return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "20").Replace("{color}", TankColor));
                    }
                    else if (WaterVolumePercentage >= 30 && WaterVolumePercentage < 40)
                    {
                        return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "30").Replace("{color}", TankColor));
                    }
                    else if (WaterVolumePercentage >= 40 && WaterVolumePercentage < 50)
                    {
                        return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "40").Replace("{color}", TankColor));
                    }
                    else if (WaterVolumePercentage >= 50 && WaterVolumePercentage < 60)
                    {
                        return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "50").Replace("{color}", TankColor));
                    }
                    else if (WaterVolumePercentage >= 60 && WaterVolumePercentage < 70)
                    {
                        return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "60").Replace("{color}", TankColor));
                    }
                    else if (WaterVolumePercentage >= 70 && WaterVolumePercentage < 80)
                    {
                        return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "70").Replace("{color}", TankColor));
                    }
                    else if (WaterVolumePercentage >= 80 && WaterVolumePercentage < 90)
                    {
                        return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "80").Replace("{color}", TankColor));
                    }
                    else if (WaterVolumePercentage >= 90 && WaterVolumePercentage < 100)
                    {
                        return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "90").Replace("{color}", TankColor));
                    }
                    else if (WaterVolumePercentage >= 100)
                    {
                        return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "100").Replace("{color}", TankColor));
                    }

                    return String.Format("{0}/{1}/{2}", baseurl, TankModelId, TankModelImage.Replace("{info}", "NoData").Replace("-{color}", ""));
                }

                return String.Empty;
            }
            private set { }
        }

        private String TankColor
        {
            get
            {
                if (HasAlarmCritical)
                    return "red";
                else if (HasAlarmHigh)
                    return "red";
                else if (HasAlarmMedium)
                    return "red";
                else if (HasAlarmLow)
                    return "red";
                else if (HasAlarmInfo)
                    return "red";
                else
                    return "blue";
            }
        }

        public Decimal WaterVolumeCapacity { get; set; }

        public Decimal? WaterVolumeRemaining
        {
            get
            {
                if (WaterVolumeLastValue.HasValue)
                    return WaterVolumeCapacity - WaterVolumeLastValue.Value;
                return null;
            }
            private set { }
        }


        #endregion Tank

        #region Last Water Volume

        public Guid? WaterVolumeLastEventId { get; set; }
        public Decimal? WaterVolumeLastValue { get; set; }
        public DateTime? WaterVolumeLastEventDate { get; set; }

        public Int32? WaterVolumePercentage
        {
            get
            {
                Decimal? perc = 0;

                if (WaterVolumeCapacity != 0 && WaterVolumeLastValue.HasValue)
                {
                    perc = (WaterVolumeLastValue / WaterVolumeCapacity) * 100;
                    return (Int32)perc;
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
        public Guid? WaterTemperatureLastEventId { get; set; }
        public Decimal? WaterTemperatureLastEventValue { get; set; }
        public DateTime? WaterTemperatureLastEventDate { get; set; }

        public Guid? AmbientTemperatureLastEventId { get; set; }
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

        #region Map

        public static List<TankViewModel> Map(IList<Core.Entities.Tank> entities)
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
            var viewModel = Mapper.Map<Core.Entities.Tank, TankViewModel>(entity);
            return viewModel;
        }

        #endregion Map  
    }
}