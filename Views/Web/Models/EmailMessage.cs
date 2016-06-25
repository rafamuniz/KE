using Microsoft.AspNet.Identity;
using System;

namespace KarmicEnergy.Web.Models
{
    public class EmailMessage : IdentityMessage
    {
        public String DestinationName { get; set; }
    }
}