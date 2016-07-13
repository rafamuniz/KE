using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("SensorGroups", Schema = "dbo")]
    public class SensorGroup : BaseEntity
    {
        #region Constructor
        public SensorGroup()
        {

        }
        #endregion Constructor

        #region Property

        [Key, Column("Id", Order = 1, TypeName = "UNIQUEIDENTIFIER")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("Weight", TypeName = "INT")]
        public Int32 Weight { get; set; }

        #endregion Property     

        #region Sensor

        [Column("SensorId", TypeName = "UNIQUEIDENTIFIER")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public Guid SensorId { get; set; }

        [ForeignKey("SensorId")]
        public virtual Sensor Sensor { get; set; }

        #endregion Sensor   

        #region Group

        [Column("GroupId", TypeName = "UNIQUEIDENTIFIER")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public Guid GroupId { get; set; }

        [ForeignKey("GroupId")]
        public virtual Group Group { get; set; }

        #endregion Group                

        #region Functions 
        public void Update(SensorGroup entity)
        {
            this.Weight = entity.Weight;
            
            this.SensorId = entity.SensorId;
            this.GroupId = entity.GroupId;

            this.CreatedDate = entity.CreatedDate;
            this.LastModifiedDate = entity.LastModifiedDate;
            this.DeletedDate = entity.DeletedDate;
        }
        #endregion Functions 
    }
}
