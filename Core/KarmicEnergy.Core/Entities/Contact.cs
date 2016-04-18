using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("Contacts", Schema = "dbo")]
    public class Contact : BaseEntity
    {
        #region Property

        [Key, Column("Id", Order = 1, TypeName = "UNIQUEIDENTIFIER")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("Email", TypeName = "NVARCHAR")]
        [StringLength(256)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{2} cannot be null or empty")]
        public String Email { get; set; }

        /// <summary>
        /// Based on https://en.wikipedia.org/wiki/E.164
        /// </summary>
        [Column("PhoneNumberCountryCode", TypeName = "NVARCHAR")]
        [StringLength(3)]
        public String PhoneNumberCountryCode { get; set; }

        /// <summary>
        /// Based on https://en.wikipedia.org/wiki/E.164
        /// </summary>
        [Column("PhoneNumber", TypeName = "NVARCHAR")]
        [StringLength(16)]
        public String PhoneNumber { get; set; }

        /// <summary>
        /// Based on https://en.wikipedia.org/wiki/E.164
        /// </summary>
        [Column("MobileNumberCountryCode", TypeName = "NVARCHAR")]
        [StringLength(3)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{2} cannot be null or empty")]
        public String MobileNumberCountryCode { get; set; }

        /// <summary>
        /// Based on https://en.wikipedia.org/wiki/E.164
        /// </summary>
        [Column("MobileNumber", TypeName = "NVARCHAR")]
        [StringLength(16)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{2} cannot be null or empty")]
        public String MobileNumber { get; set; }

        [Column("AddressLine1", TypeName = "NVARCHAR")]
        [StringLength(256)]
        public String AddressLine1 { get; set; }

        [Column("AddressLine2", TypeName = "NVARCHAR")]
        [StringLength(256)]
        public String AddressLine2 { get; set; }

        [Column("City", TypeName = "NVARCHAR")]
        [StringLength(128)]
        public String City { get; set; }

        [Column("State", TypeName = "NVARCHAR")]
        [StringLength(64)]
        public String State { get; set; }

        [Column("Country", TypeName = "NVARCHAR")]
        [StringLength(64)]
        public String Country { get; set; } = "United States";

        [Column("ZipCode", TypeName = "NVARCHAR")]
        [StringLength(16)]
        public String ZipCode { get; set; }

        #endregion Property

        public virtual IList<Customer> Customers { get; set; }

        public virtual IList<CustomerUser> CustomerUsers { get; set; }
    }
}
