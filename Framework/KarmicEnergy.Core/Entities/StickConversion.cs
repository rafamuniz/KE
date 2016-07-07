using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("StickConversions", Schema = "dbo")]
    public class StickConversion : BaseEntity
    {
        #region Property

        [Key, Column("Id", Order = 1, TypeName = "INT")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Int32 Id { get; set; }

        [Column("Name", TypeName = "NVARCHAR")]
        [StringLength(128)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public String Name { get; set; }

        [Column("Status", TypeName = "CHAR")]
        [StringLength(1)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public String Status { get; set; } = "A";

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

        #endregion Property    

        #region Load

        public static List<StickConversion> Load()
        {
            List<StickConversion> entities = new List<StickConversion>()
            {
                new StickConversion() { Id = 1, Name = "500 BBL 2100 Series", FromUnitId = 7, ToUnitId = 2 },
                new StickConversion() { Id = 2, Name = "500 BBL FT Frac Tank", FromUnitId = 7, ToUnitId = 2 },
                new StickConversion() { Id = 3, Name = "500 BBL H Frac Tank", FromUnitId = 7, ToUnitId = 2 },
                new StickConversion() { Id = 4, Name = "510 BBL N Frac Tank", FromUnitId = 7, ToUnitId = 2 },
                new StickConversion() { Id = 5, Name = "500 BBL S Frac Tank", FromUnitId = 7, ToUnitId = 2 },
                new StickConversion() { Id = 6, Name = "500 BBL W Frac Tank", FromUnitId = 7, ToUnitId = 2 },
                new StickConversion() { Id = 7, Name = "Flowback Tanks", FromUnitId = 7, ToUnitId = 2 },
            };

            return entities;
        }
        #endregion Load
    }
}