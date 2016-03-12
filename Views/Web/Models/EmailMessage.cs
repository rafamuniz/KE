using Microsoft.AspNet.Identity;
using System;

namespace KarmicEnergy.Web.Models
{
    public class EmailMessage : IdentityMessage
    {
        public String TemplateId { get; set; }
    }
}