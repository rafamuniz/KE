using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("TriggerContacts", Schema = "dbo")]
    public class TriggerContact : BaseEntity
    {
        #region Property

        [Key, Column("Id", Order = 1, TypeName = "UNIQUEIDENTIFIER")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("Status", TypeName = "CHAR")]
        [StringLength(1)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public String Status { get; set; } = "A";

        #endregion Property

        #region Trigger

        [Column("TriggerId", TypeName = "UNIQUEIDENTIFIER")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public Guid TriggerId { get; set; }

        [ForeignKey("TriggerId")]
        public virtual Trigger Trigger { get; set; }

        #endregion Trigger

        #region Contact

        [Column("ContactId", TypeName = "UNIQUEIDENTIFIER")]
        public Guid? ContactId { get; set; }

        #endregion Contact   

        #region User

        [Column("UserId", TypeName = "UNIQUEIDENTIFIER")]
        public Guid? UserId { get; set; }

        #endregion User 

        #region Functions
        public void Update(TriggerContact entity)
        {
            this.Status = entity.Status;

            this.TriggerId = entity.TriggerId;
            this.ContactId = entity.ContactId;
            this.UserId = entity.UserId;

            this.CreatedDate = entity.CreatedDate;
            this.LastModifiedDate = entity.LastModifiedDate;
            this.DeletedDate = entity.DeletedDate;
        }

        #endregion Functions
    }
}
