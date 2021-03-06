﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace KarmicEnergy.Web.Areas.Admin.ViewModels.TankModel
{
    public class CreateViewModel
    {
        #region Property

        [Display(Name = "Name")]
        [Required]
        public String Name { get; set; }

        [Display(Name = "Status")]
        [Required]
        public String Status { get; set; }

        //[Display(Name = "Image")]
        //[Required]
        //public Byte[] Image { get; set; }

        public HttpPostedFileBase Image { get; set; }

        #endregion Property
    }
}