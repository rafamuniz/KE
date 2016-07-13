using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("SensorTypes", Schema = "dbo")]
    public class SensorType : BaseEntity
    {
        #region Property

        [Key, Column("Id", Order = 1, TypeName = "SMALLINT")]
        public Int16 Id { get; set; }

        [Column("Name", TypeName = "NVARCHAR")]
        [StringLength(128)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public String Name { get; set; }

        [Column("Status", TypeName = "CHAR")]
        [StringLength(1)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public String Status { get; set; } = "A";

        #endregion Property        

        #region Item

        public virtual List<Item> Items { get; set; }

        #endregion Item          

        #region Functions

        public static List<SensorType> Load()
        {
            List<SensorType> entities = new List<SensorType>()
            {
                new SensorType() { Id = (Int16)SensorTypeEnum.KEDepth, Name = "KE Depth Sensor" },
                new SensorType() { Id = (Int16)SensorTypeEnum.FlowMeter, Name = "Flow Meter" },
                new SensorType() { Id = (Int16)SensorTypeEnum.PHMeter, Name = "PH Meter" },
                new SensorType() { Id = (Int16)SensorTypeEnum.GasSensor, Name = "Gas Sensor" },
                new SensorType() { Id = (Int16)SensorTypeEnum.SalinitySensor, Name = "Salinity Sensor" }
            };

            return entities;
        }

        public void Update(SensorType entity)
        {
            this.Name = entity.Name;
            this.Status = entity.Status;
            this.CreatedDate = entity.CreatedDate;
            this.LastModifiedDate = entity.LastModifiedDate;
            this.DeletedDate = entity.DeletedDate;
        }
        #endregion Functions
    }

    public enum SensorTypeEnum : short
    {
        KEDepth = 1,
        FlowMeter = 2,
        PHMeter = 3,
        GasSensor = 4,
        SalinitySensor = 5
    }
}
