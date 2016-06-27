using AutoMapper;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.FastTracker
{
    public class WaterQualityViewModel
    {
        #region Constructor
        public WaterQualityViewModel()
        {

        }
        #endregion Constructor

        #region Property
        public Guid? ChloridesLastEventId { get; set; }
        public Decimal? ChloridesLastEventValue { get; set; }
        public Decimal? ChloridesLastEventDate { get; set; }
        public String ChloridesSymbol { get; set; }

        public Guid? PHLastEventId { get; set; }
        public Decimal? PHLastEventValue { get; set; }
        public DateTime? PHLastEventDate { get; set; }
        public String PHSymbol { get; set; }

        public Guid? TemperatureAmbientLastEventId { get; set; }
        public Decimal? TemperatureAmbientLastEventValue { get; set; }
        public DateTime? TemperatureAmbientLastEventDate { get; set; }
        public String TemperatureAmbientSymbol { get; set; }

        public Guid? TemperatureWaterLastEventId { get; set; }
        public Decimal? TemperatureWaterLastEventValue { get; set; }
        public DateTime? TemperatureWaterLastEventDate { get; set; }
        public String TemperatureWaterSymbol { get; set; }

        #endregion Property        

        #region Map

        #endregion Map      
    }
}