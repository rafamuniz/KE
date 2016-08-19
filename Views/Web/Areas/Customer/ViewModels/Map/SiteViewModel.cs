using KarmicEnergy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using KarmicEnergy.Web.ViewModels;
using AutoMapper;
using Munizoft.Extensions;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Map
{
    public class SiteViewModel
    {
        #region Constructor
        public SiteViewModel()
        {
            Alarms = new List<AlarmViewModel>();
        }
        #endregion Constructor

        #region Property

        #region Site
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Reference { get; set; }
        public String Latitude { get; set; }
        public String Longitude { get; set; }
        public String IPAddress { get; set; }

        #endregion Site

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

        public static List<SiteViewModel> Map(IEnumerable<Core.Entities.Site> entities)
        {
            List<SiteViewModel> vms = new List<SiteViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(SiteViewModel.Map(c)));
            }

            return vms;
        }

        public static SiteViewModel Map(Core.Entities.Site entity)
        {
            var viewModel = Mapper.Map<Core.Entities.Site, SiteViewModel>(entity);
            return viewModel;
        }

        #endregion Map  
    }
}