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

        [Column("Action", TypeName = "NVARCHAR")]
        [StringLength(256)]
        public String Action { get; set; } = String.Empty;

        [Column("Message", TypeName = "NVARCHAR")]
        [StringLength(4000)]
        public String Message { get; set; } = String.Empty;

        [Column("Value", TypeName = "NVARCHAR")]
        [StringLength(256)]
        public String Value { get; set; } = String.Empty;

        [Column("CalculatedValue", TypeName = "NVARCHAR")]
        [StringLength(256)]
        public String CalculatedValue { get; set; } = String.Empty;

        [Column("UserId", TypeName = "UNIQUEIDENTIFIER")]
        public Guid UserId { get; set; }
                
        #endregion Property

        #region Alarm

        [Column("AlarmId", TypeName = "UNIQUEIDENTIFIER")]
        public Guid AlarmId { get; set; }

        #endregion Alarm        
    }
}
