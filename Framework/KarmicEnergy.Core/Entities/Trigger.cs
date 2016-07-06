using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("Triggers", Schema = "dbo")]
    public class Trigger : BaseEntity
    {
        #region Constructor
        public Trigger()
        {
            Contacts = new List<TriggerContact>();
        }

        #endregion Constructor

        #region Property

        [Key, Column("Id", Order = 1, TypeName = "UNIQUEIDENTIFIER")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("Value", TypeName = "NVARCHAR")]
        [StringLength(256)]
        public String Value { get; set; }

        //[Column("MinValue", TypeName = "NVARCHAR")]
        //[StringLength(256)]
        //public String MinValue { get; set; }

        //[Column("MaxValue", TypeName = "NVARCHAR")]
        //[StringLength(256)]
        //public String MaxValue { get; set; }

        [Column("Status", TypeName = "CHAR")]
        [StringLength(1)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public String Status { get; set; } = "A";

        #endregion Property

        #region Sensor Item

        [Column("SensorItemId", TypeName = "UNIQUEIDENTIFIER")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public Guid SensorItemId { get; set; }

        [ForeignKey("SensorItemId")]
        public virtual SensorItem SensorItem { get; set; }

        #endregion Sensor Item   

        #region Severity

        [Column("SeverityId", TypeName = "SMALLINT")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public Int16 SeverityId { get; set; }

        [ForeignKey("SeverityId")]
        public virtual Severity Severity { get; set; }

        #endregion Severity   

        #region Contacts

        public virtual List<TriggerContact> Contacts { get; set; }

        #endregion Contacts  

        #region Alarms

        public virtual List<Alarm> Alarms { get; set; }

        [NotMapped]
        public Boolean HasAlarm
        {
            get
            {
                return Alarms.Where(x => x.EndDate == null).Any();
            }
        }

        #endregion Alarms  

        #region Operator

        [Column("OperatorId", TypeName = "SMALLINT")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public Int16 OperatorId { get; set; }

        [ForeignKey("OperatorId")]
        public virtual Operator Operator { get; set; }

        #endregion Operator

        #region Functions
        public Boolean IsAlarm(String value)
        {
            Decimal eventValue;

            if (!Decimal.TryParse(value, out eventValue))
            {
                throw new ArgumentException("Event Value invalid");
            }

            Decimal triggerValue;

            if (!Decimal.TryParse(this.Value, out triggerValue))
            {
                throw new ArgumentException("Value invalid");
            }

            switch (this.Operator.Symbol)
            {
                case "=":
                    if (eventValue == triggerValue)
                        return true;
                    else
                        return false;
                case "<>":
                    if (eventValue != triggerValue)
                        return true;
                    else
                        return false;
                case ">":
                    if (eventValue > triggerValue)
                        return true;
                    else
                        return false;
                case "<":
                    if (eventValue < triggerValue)
                        return true;
                    else
                        return false;
                case ">=":
                    if (eventValue >= triggerValue)
                        return true;
                    else
                        return false;
                case "<=":
                    if (eventValue <= triggerValue)
                        return true;
                    else
                        return false;
                default:
                    throw new ArgumentException("Value wrong");
            }
        }

        #endregion Functions
    }
}
