using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("Operators", Schema = "dbo")]
    public class Operator : BaseEntity
    {
        #region Property
        [Key, Column("Id", Order = 1, TypeName = "SMALLINT")]        
        public Int16 Id { get; set; }

        [Column("Name", TypeName = "NVARCHAR")]
        [StringLength(32)]
        public String Name { get; set; }

        [Column("Symbol", TypeName = "NVARCHAR")]
        [StringLength(16)]
        public String Symbol { get; set; }

        [Column("Description", TypeName = "NVARCHAR")]
        [StringLength(512)]
        public String Description { get; set; }

        #endregion Property

        #region OperatorType

        [Column("OperatorTypeId", TypeName = "SMALLINT")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public Int16 OperatorTypeId { get; set; }

        [ForeignKey("OperatorTypeId")]
        public virtual OperatorType OperatorType { get; set; }

        #endregion OperatorType

        #region Functions

        public static List<Operator> Load()
        {
            List<Operator> entities = new List<Operator>()
            {
                new Operator() { Id = 1, Symbol = "=", OperatorTypeId = (Int16)OperatorTypeEnum.Relational, Name = "Equal", Description = "Checks if the values of two operands are equal or not, if yes then condition becomes true." },
                new Operator() { Id = 2, Symbol = "<>", OperatorTypeId = (Int16)OperatorTypeEnum.Relational, Name = "Not Equal", Description = "Checks if the values of two operands are equal or not, if values are not equal then condition becomes true." },
                new Operator() { Id = 3, Symbol = ">", OperatorTypeId = (Int16)OperatorTypeEnum.Relational, Name = "Greater than", Description = "Checks if the value of left operand is greater than the value of right operand, if yes then condition becomes true." },
                new Operator() { Id = 4, Symbol = "<", OperatorTypeId = (Int16)OperatorTypeEnum.Relational, Name = "Less than", Description = "Checks if the value of left operand is less than the value of right operand, if yes then condition becomes true." },
                new Operator() { Id = 5, Symbol = ">=", OperatorTypeId = (Int16)OperatorTypeEnum.Relational, Name = "Greater than or Equal", Description = "Checks if the value of left operand is greater than or equal to the value of right operand, if yes then condition becomes true." },
                new Operator() { Id = 6, Symbol = "<=", OperatorTypeId = (Int16)OperatorTypeEnum.Relational, Name = "Less than or Equal", Description = "Checks if the value of left operand is less than or equal to the value of right operand, if yes then condition becomes true." },

                new Operator() { Id = 7, Symbol = "&&", OperatorTypeId = (Int16)OperatorTypeEnum.Logical, Name = "And", Description = "Called Logical AND operator. If both the operands are non-zero, then condition becomes true." },
                new Operator() { Id = 8, Symbol = "||", OperatorTypeId = (Int16)OperatorTypeEnum.Logical, Name = "Or", Description = "Called Logical OR Operator. If any of the two operands is non-zero, then condition becomes true." },
                new Operator() { Id = 9, Symbol = "!", OperatorTypeId = (Int16)OperatorTypeEnum.Logical, Name = "Not", Description = "Called Logical NOT Operator. Use to reverses the logical state of its operand. If a condition is true, then Logical NOT operator will make false." },
            };

            return entities;
        }
        public void Update(Operator entity)
        {
            this.Name = entity.Name;
            this.Symbol = entity.Symbol;
            this.Description = entity.Description;
            this.OperatorTypeId = entity.OperatorTypeId;
                      
            this.CreatedDate = entity.CreatedDate;
            this.LastModifiedDate = entity.LastModifiedDate;
            this.DeletedDate = entity.DeletedDate;
        }
        #endregion Functions
    }
}