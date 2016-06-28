using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("AlarmHistories", Schema = "dbo")]
    public class AlarmHistory : BaseEntity
    {
        #region Property

        [Key, Column("Id", Order = 1, TypeName = "UNIQUEIDENTIFIER")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
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
    }
}
