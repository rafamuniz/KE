using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("StickConversions", Schema = "dbo")]
    public class StickConversion : BaseEntity
    {
        #region Property

        [Key, Column("Id", Order = 1, TypeName = "INT")]
        public Int32 Id { get; set; }

        [Column("Name", TypeName = "NVARCHAR")]
        [StringLength(128)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public String Name { get; set; }

        [Column("Status", TypeName = "CHAR")]
        [StringLength(1)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public String Status { get; set; } = "A";

        #endregion Property      

        #region From Unit

        [Column("FromUnitId", TypeName = "SMALLINT")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public Int16 FromUnitId { get; set; }

        [ForeignKey("FromUnitId")]
        public virtual Unit FromUnit { get; set; }

        #endregion From Unit

        #region To Unit

        [Column("ToUnitId", TypeName = "SMALLINT")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public Int16 ToUnitId { get; set; }

        [ForeignKey("ToUnitId")]
        public virtual Unit ToUnit { get; set; }

        #endregion To Unit
    }
}