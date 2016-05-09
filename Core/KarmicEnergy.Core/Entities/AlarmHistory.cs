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

        [Column("Value", TypeName = "NVARCHAR")]
        [StringLength(256)]
        public String Value { get; set; } = String.Empty;

        [Column("CalculatedValue", TypeName = "NVARCHAR")]
        [StringLength(256)]
        public String CalculatedValue { get; set; } = String.Empty;

        [Column("AckUserId", TypeName = "UNIQUEIDENTIFIER")]
        public Guid AckUserId { get; set; }

        [Column("AckDate", TypeName = "DATETIME")]
        public DateTime AckDate { get; set; }

        #endregion Property

        #region Alarm

        [Column("AlarmId", TypeName = "UNIQUEIDENTIFIER")]
        public Guid AlarmId { get; set; }

        #endregion Alarm        
    }
}
