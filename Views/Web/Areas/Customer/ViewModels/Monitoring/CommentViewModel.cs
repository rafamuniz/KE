using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KarmicEnergy.Web.Areas.Customer.ViewModels.Monitoring
{
    public class CommentViewModel
    {
        #region Constructor
        public CommentViewModel()
        {

        }
        #endregion Constructor

        #region Property

        public Guid Id { get; set; }

        public String Message { get; set; }

        public Guid UserId { get; set; }
        public String UserName { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime CreatedDateLocal
        {
            get { return CreatedDate.ToLocalTime(); }
            set { }
        }

        #endregion Property

        #region Map

        public static List<CommentViewModel> Map(List<Core.Entities.AlarmHistory> entities)
        {
            List<CommentViewModel> vms = new List<CommentViewModel>();

            if (entities != null && entities.Any())
            {
                entities.ForEach(c => vms.Add(CommentViewModel.Map(c)));
            }

            return vms;
        }

        public static CommentViewModel Map(Core.Entities.AlarmHistory entity)
        {            
            var viewModel = Mapper.Map<Core.Entities.AlarmHistory, CommentViewModel>(entity);
                        
            viewModel.UserId = entity.UserId;
            viewModel.UserName = entity.UserName;

            return viewModel;
        }

        #endregion Map
    }
}