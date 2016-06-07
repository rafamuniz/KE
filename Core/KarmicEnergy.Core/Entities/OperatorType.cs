using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("OperatorTypes", Schema = "dbo")]
    public class OperatorType : BaseEntity
    {
        #region Property

        [Key, Column("Id", Order = 1, TypeName = "SMALLINT")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int16 Id { get; set; }

        [Column("Name", TypeName = "NVARCHAR")]
        [StringLength(32)]
        public String Name { get; set; }

        #endregion Property

        #region Load

        public static List<OperatorType> Load()
        {
            List<OperatorType> entities = new List<OperatorType>()
            {
                new OperatorType() { Id = (Int16)OperatorTypeEnum.Relational, Name = "Relational" },
                new OperatorType() { Id = (Int16)OperatorTypeEnum.Logical, Name = "Logical" },               
            };

            return entities;
        }
        #endregion Load
    }

    public enum OperatorTypeEnum : short
    {
        Relational = 1,
        Logical = 2     
    }
}
