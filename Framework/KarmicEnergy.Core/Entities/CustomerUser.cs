using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("CustomerUsers", Schema = "dbo")]
    public class CustomerUser : BaseEntity
    {
        #region Constructor
        public CustomerUser()
        {
            this.Id = Guid.NewGuid();
            Sites = new List<CustomerUserSite>();
            Settings = new List<CustomerUserSetting>();

        }
        #endregion Constructor

        #region Property

        [Key, Column("Id", Order = 1, TypeName = "UNIQUEIDENTIFIER")]
        public Guid Id { get; set; }

        #endregion Property

        #region Customer

        [Column("CustomerId", TypeName = "UNIQUEIDENTIFIER")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{2} cannot be null or empty")]
        public Guid CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        #endregion Customer

        #region Address

        [Column("AddressId", TypeName = "UNIQUEIDENTIFIER")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public Guid AddressId { get; set; }

        [ForeignKey("AddressId")]
        public virtual Address Address { get; set; }

        #endregion Address

        #region Settings        

        public virtual IList<CustomerUserSetting> Settings { get; set; }

        #endregion Settings

        #region Settings        

        public virtual IList<CustomerUserSite> Sites { get; set; }

        #endregion Settings

        #region Functions
        public void Update(CustomerUser entity)
        {
            this.CustomerId = entity.CustomerId;
            this.AddressId = entity.AddressId;

            this.CreatedDate = entity.CreatedDate;
            this.LastModifiedDate = entity.LastModifiedDate;
            this.DeletedDate = entity.DeletedDate;
        }
        #endregion Functions
    }
}
