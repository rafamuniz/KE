﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;

namespace KarmicEnergy.Core.Entities
{
    [Table("Triggers", Schema = "dbo")]
    public class Trigger : BaseEntity
    {
        #region Constructor
        public Trigger()
        {
            this.Id = Guid.NewGuid();
            Contacts = new List<TriggerContact>();
        }

        #endregion Constructor

        #region Property

        [Key, Column("Id", Order = 1, TypeName = "UNIQUEIDENTIFIER")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [Column("Value", TypeName = "NVARCHAR")]
        [StringLength(256)]
        public String Value { get; set; }

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
        [IgnoreDataMember]
        public Boolean HasAlarm
        {
            get
            {
                if (Alarms.Any())
                    return Alarms.Where(x => x.EndDate == null).Any();
                else
                    return false;
            }
            set { }
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

        public void Update(Trigger entity)
        {
            this.Status = entity.Status;
            this.Value = entity.Value;
            this.SensorItemId = entity.SensorItemId;
            this.SeverityId = entity.SeverityId;
            this.OperatorId = entity.OperatorId;

            this.CreatedDate = entity.CreatedDate;
            this.LastModifiedDate = entity.LastModifiedDate;
            this.DeletedDate = entity.DeletedDate;
        }

        #endregion Functions
    }
}
