using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("AlarmHistories", Schema = "dbo")]
    public class AlarmHistory : BaseEntity
    {
        #region Constructor
        public AlarmHistory()
        {
            this.Id = Guid.NewGuid();
        }
        #endregion Constructor

        #region Property

        [Key, Column("Id", Order = 1, TypeName = "UNIQUEIDENTIFIER")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [Column("Message", TypeName = "NVARCHAR")]
        [StringLength(4000)]
        public String Message { get; set; } = String.Empty;

        [Column("Value", TypeName = "NVARCHAR")]
        [StringLength(256)]
        public String Value { get; set; } = String.Empty;

        [Column("UserId", TypeName = "UNIQUEIDENTIFIER")]
        public Guid UserId { get; set; }

        [Column("UserName", TypeName = "NVARCHAR")]
        [StringLength(256)]
        public String UserName { get; set; }

        #endregion Property

        #region Alarm

        [Column("AlarmId", TypeName = "UNIQUEIDENTIFIER")]
        public Guid AlarmId { get; set; }

        #endregion Alarm        

        #region ActionType

        [Column("ActionTypeId", TypeName = "SMALLINT")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public Int16 ActionTypeId { get; set; } = (Int16)ActionTypeEnum.Info;

        [ForeignKey("ActionTypeId")]
        public virtual ActionType ActionType { get; set; }

        #endregion ActionType

        #region functions
        public void Update(AlarmHistory entity)
        {
            this.AlarmId = entity.AlarmId;
            this.ActionTypeId = entity.ActionTypeId;

            this.Message = entity.Message;
            this.Value = entity.Value;
            this.UserName = entity.UserName;
            this.UserId = entity.UserId;
 
            this.CreatedDate = entity.CreatedDate;
            this.LastModifiedDate = entity.LastModifiedDate;
            this.DeletedDate = entity.DeletedDate;
        }

        #endregion functions
    }
}
