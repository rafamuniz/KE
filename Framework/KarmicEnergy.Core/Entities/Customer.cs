using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("Customers", Schema = "dbo")]
    public class Customer : BaseEntity
    {
        #region Property

        [Key, Column("Id", Order = 1, TypeName = "UNIQUEIDENTIFIER")]
        public Guid Id { get; set; }

        #endregion Property

        #region Address

        [Column("AddressId", TypeName = "UNIQUEIDENTIFIER")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public Guid AddressId { get; set; }

        [ForeignKey("AddressId")]
        public virtual Address Address { get; set; }

        #endregion Address

        #region Settings        

        public virtual IList<CustomerSetting> Settings { get; set; }

        #endregion Settings

        #region CustomerUsers        

        public virtual IList<CustomerUser> Users { get; set; }

        #endregion CustomerUsers

        #region Functions
        public void Update(Customer entity)
        {            
            this.AddressId = entity.AddressId;

            this.CreatedDate = entity.CreatedDate;
            this.LastModifiedDate = entity.LastModifiedDate;
            this.DeletedDate = entity.DeletedDate;
        }
        #endregion Functions
    }
}
