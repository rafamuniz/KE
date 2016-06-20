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
        public Decimal? ChloridesLastEventValue { get; set; }
        public Decimal? ChloridesLastEventDate { get; set; }
        public String ChloridesSymbol { get; set; }

        public Decimal? PHLastEventValue { get; set; }
        public DateTime? PHLastEventDate { get; set; }
        public String PHSymbol { get; set; }

        public Decimal? TemperatureAmbientLastEventValue { get; set; }
        public DateTime? TemperatureAmbientLastEventDate { get; set; }
        public String TemperatureAmbientSymbol { get; set; }

        #endregion Property        

        #region Map

        //public static List<WaterQualityViewModel> Map(List<Core.Entities.Alarm> entities)
        //{
        //    List<WaterQualityViewModel> vms = new List<WaterQualityViewModel>();

        //    if (entities != null && entities.Any())
        //    {
        //        entities.ForEach(c => vms.Add(WaterQualityViewModel.Map(c)));
        //    }

        //    return vms;
        //}

        //public static WaterQualityViewModel Map(Core.Entities.Alarm entity)
        //{
        //    Mapper.CreateMap<Core.Entities.Alarm, WaterQualityViewModel>();
        //    var viewModel = Mapper.Map<Core.Entities.Alarm, WaterQualityViewModel>(entity);

        //    return viewModel;
        //}

        #endregion Map      
    }
}