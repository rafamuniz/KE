using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Admin.ViewModels.Sync
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

        public String Action { get; set; }

        public DateTime SyncDate { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime StartDateLocal
        {
            get { return StartDate.ToLocalTime(); }
            set { }
        }

        public DateTime? EndDate { get; set; }
        public DateTime? EndDateLocal
        {
            get
            {
                if (EndDate.HasValue)
                    return EndDate.Value.ToLocalTime();
                return null;
            }
            set { }
        }

        public Int32? DurationMin
        {
            get
            {
                if (EndDate.HasValue)
                    return EndDate.Value.Subtract(StartDate).Minutes;
                return null;
            }
        }

        public DateTime LogDate { get; set; }
        public DateTime LogDateLocal
        {
            get { return LogDate.ToLocalTime(); }
            set { }
        }
        #endregion Property

        #region Map

        public static List<ListViewModel> Map(List<Core.Entities.DataSync> entities)
        {
            List<ListViewModel> vms = new List<ListViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(ListViewModel.Map(c)));
            }

            return vms;
        }

        public static ListViewModel Map(Core.Entities.DataSync entity)
        {
            var viewModel = Mapper.Map<Core.Entities.DataSync, ListViewModel>(entity);

            return viewModel;
        }

        #endregion Map
    }
}