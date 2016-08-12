using AutoMapper;
using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Munizoft.Extensions;
using KarmicEnergy.Web.ViewModels;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Map
{
    public class PondViewModel
    {
        #region Constructor
        public PondViewModel()
        {
            WaterVolumes = new List<WaterVolumeViewModel>();
            //Alarms = new List<AlarmViewModel>();
        }
        #endregion Constructor

        #region Property

        #region Pond
        public Guid SiteId { get; set; }

        public Guid Id { get; set; }

        public String Name { get; set; }

        public String UrlImage
        {
            get
            {
                var baseurl = "/images/pond/";
                var image_name = "FracPO1-{color}-{info}.png";

                if (!WaterVolumePercentage.HasValue)
                    return String.Format("{0}/{1}", baseurl, image_name.Replace("{info}", "nodata").Replace("-{color}", ""));

                if (WaterVolumePercentage <= 0)
                {
                    return String.Format("{0}/{1}", baseurl, image_name.Replace("{info}", "empty").Replace("-{color}", ""));
                }
                else if (WaterVolumePercentage > 0 && WaterVolumePercentage < 20)
                {
                    return String.Format("{0}/{1}", baseurl, image_name.Replace("{info}", "10").Replace("{color}", Color));
                }
                else if (WaterVolumePercentage >= 20 && WaterVolumePercentage < 30)
                {
                    return String.Format("{0}/{1}", baseurl, image_name.Replace("{info}", "20").Replace("{color}", Color));
                }
                else if (WaterVolumePercentage >= 30 && WaterVolumePercentage < 40)
                {
                    return String.Format("{0}/{1}", baseurl, image_name.Replace("{info}", "30").Replace("{color}", Color));
                }
                else if (WaterVolumePercentage >= 40 && WaterVolumePercentage < 50)
                {
                    return String.Format("{0}/{1}", baseurl, image_name.Replace("{info}", "40").Replace("{color}", Color));
                }
                else if (WaterVolumePercentage >= 50 && WaterVolumePercentage < 60)
                {
                    return String.Format("{0}/{1}", baseurl, image_name.Replace("{info}", "50").Replace("{color}", Color));
                }
                else if (WaterVolumePercentage >= 60 && WaterVolumePercentage < 70)
                {
                    return String.Format("{0}/{1}", baseurl, image_name.Replace("{info}", "60").Replace("{color}", Color));
                }
                else if (WaterVolumePercentage >= 70 && WaterVolumePercentage < 80)
                {
                    return String.Format("{0}/{1}", baseurl, image_name.Replace("{info}", "70").Replace("{color}", Color));
                }
                else if (WaterVolumePercentage >= 80 && WaterVolumePercentage < 90)
                {
                    return String.Format("{0}/{1}", baseurl, image_name.Replace("{info}", "80").Replace("{color}", Color));
                }
                else if (WaterVolumePercentage >= 90 && WaterVolumePercentage < 100)
                {
                    return String.Format("{0}/{1}", baseurl, image_name.Replace("{info}", "90").Replace("{color}", Color));
                }
                else if (WaterVolumePercentage >= 100)
                {
                    return String.Format("{0}/{1}", baseurl, image_name.Replace("{info}", "100").Replace("{color}", Color));
                }

                return String.Format("{0}/{1}", baseurl, image_name.Replace("{info}", "NoData").Replace("-{color}", ""));
            }
            private set { }
        }

        private String Color
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

        #endregion Tank

        #region Last Water Volume

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

        public static List<PondViewModel> Map(IList<Core.Entities.Pond> entities)
        {
            List<PondViewModel> vms = new List<PondViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(PondViewModel.Map(c)));
            }

            return vms;
        }

        public static PondViewModel Map(Core.Entities.Pond entity)
        {
            var viewModel = Mapper.Map<Core.Entities.Pond, PondViewModel>(entity);
            return viewModel;
        }

        #endregion Map      
    }
}