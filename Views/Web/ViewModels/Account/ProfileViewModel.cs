using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Munizoft.Extensions;
using System.IO;
using System.Web.Hosting;

namespace KarmicEnergy.Web.ViewModels.Account
{
    public class ProfileViewModel
    {
        #region Fields
        private Byte[] photo;
        #endregion Fields

        [Display(Name = "Name")]
        public String Name { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public String Email { get; set; }

        [Display(Name = "Photo File")]
        public HttpPostedFileBase PhotoFile { get; set; }

        [Display(Name = "Photo")]
        public Byte[] Photo
        {
            get
            {
                if (photo == null)
                {
                    return File.ReadAllBytes(HostingEnvironment.MapPath("~/images/default_user_image.png"));
                }
                return photo;
            }
            set { photo = value; }
        }
    }
}