using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("Alarms", Schema = "dbo")]
    public class Alarm : BaseEntity
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

        [Column("LastAckUserId", TypeName = "UNIQUEIDENTIFIER")]
        public Guid? LastAckUserId { get; set; }

        [Column("LastAckDate", TypeName = "DATETIME")]
        public DateTime? LastAckDate { get; set; }

        [Column("StartDate", TypeName = "DATETIME")]
        [Required]
        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        [Column("EndDate", TypeName = "DATETIME")]
        public DateTime? EndDate { get; set; }

        #endregion Property

        #region Sensor Event

        [Column("SensorItemEventId", TypeName = "UNIQUEIDENTIFIER")]
        public Guid SensorItemEventId { get; set; }

        #endregion Sensor Event

        #region Trigger

        [Column("TriggerId", TypeName = "UNIQUEIDENTIFIER")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public Guid TriggerId { get; set; }

        [ForeignKey("TriggerId")]
        public virtual Trigger Trigger { get; set; }

        #endregion Trigger 
    }
}
