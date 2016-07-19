using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KarmicEnergy.Web.Entities
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Column("Name", TypeName = "NVARCHAR")]
        [StringLength(128)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public String Name { get; set; }

        [Column("Photo", TypeName = "VARBINARY")]
        [MaxLength]
        public Byte[] Photo { get; set; }

        [NotMapped]
        public IList<String> RoleNames { get; set; }

        [Column("CreatedDate", TypeName = "DATETIME")]
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Column("LastModifiedDate", TypeName = "DATETIME")]
        [Required]
        public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;

        [Column("DeletedDate", TypeName = "DATETIME")]
        public DateTime? DeletedDate { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public void Update(ApplicationUser entity)
        {
            this.AccessFailedCount = entity.AccessFailedCount;

            this.Email = entity.Email;
            this.EmailConfirmed = entity.EmailConfirmed;
            this.LockoutEnabled = entity.LockoutEnabled;

            this.LockoutEndDateUtc = entity.LockoutEndDateUtc;
            this.Name = entity.Name;
            this.PasswordHash = entity.PasswordHash;
            this.PhoneNumber = entity.PhoneNumber;
            this.PhoneNumberConfirmed = entity.PhoneNumberConfirmed;
            this.Photo = entity.Photo;
            this.SecurityStamp = entity.SecurityStamp;
            this.TwoFactorEnabled = entity.TwoFactorEnabled;
        }
    }
}