using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("StickConversionValues", Schema = "dbo")]
    public class StickConversionValue : BaseEntity
    {
        #region Property

        [Key, Column("Id", Order = 1, TypeName = "UNIQUEIDENTIFIER")]
        public Guid Id { get; set; }

        [Column("ToValue", TypeName = "NVARCHAR")]
        [StringLength(128)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public String ToValue { get; set; }

        [Column("FromValue", TypeName = "NVARCHAR")]
        [StringLength(128)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public String FromValue { get; set; }

        #endregion Property      

        #region Unit

        [Column("StickConversionId", TypeName = "INT")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public Int32 StickConversionId { get; set; }

        [ForeignKey("StickConversionId")]
        public virtual StickConversion StickConversion { get; set; }

        #endregion Unit

        #region Load

        public static List<StickConversionValue> Load(List<StickConversion> stickConversions)
        {
            List<StickConversionValue> entities = new List<StickConversionValue>();

            foreach (var sc in stickConversions)
            {
                entities.AddRange(CreateList(sc));
            }

            return entities;
        }

        public static List<StickConversionValue> CreateList(StickConversion entity)
        {
            //entity


            //return entities;
            return new List<StickConversionValue>();
        }
        #endregion Load
    }
}