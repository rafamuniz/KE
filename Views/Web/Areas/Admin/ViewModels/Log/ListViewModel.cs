using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Admin.ViewModels.Log
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

        public Guid? CustomerId { get; set; }

        public Guid? SiteId { get; set; }
        public String SiteName { get; set; }

        public Guid? UserId { get; set; }
        public String Username { get; set; }

        public Int16 LogTypeId { get; set; }
        public String LogTypeName { get; set; }

        public String Message { get; set; }

        public DateTime LogDate { get; set; }
        public DateTime LogDateLocal
        {
            get { return LogDate.ToLocalTime(); }
            set { }
        }
        #endregion Property

        #region Map

        public static List<ListViewModel> Map(List<Core.Entities.Log> entities)
        {
            List<ListViewModel> vms = new List<ListViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(ListViewModel.Map(c)));
            }

            return vms;
        }

        public static ListViewModel Map(Core.Entities.Log entity)
        {
            var viewModel = Mapper.Map<Core.Entities.Log, ListViewModel>(entity);

            viewModel.CustomerId = entity.CustomerId.HasValue ? entity.CustomerId.Value : Guid.Empty;
            viewModel.SiteId = entity.SiteId.HasValue ? entity.SiteId.Value : Guid.Empty;
            viewModel.SiteName = entity.Site != null ? entity.Site.Name : String.Empty;
            viewModel.UserId = entity.UserId.HasValue ? entity.UserId.Value : Guid.Empty;
            viewModel.LogTypeName = entity.LogType.Name;
            viewModel.LogDate = entity.CreatedDate;

            return viewModel;
        }
        
        #endregion Map
    }
}