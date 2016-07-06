using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("ActionTypes", Schema = "dbo")]
    public class ActionType : BaseEntity
    {
        #region Constructor
        public ActionType()
        {

        }
        #endregion Constructor

        #region Property

        [Key, Column("Id", Order = 1, TypeName = "SMALLINT")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int16 Id { get; set; }

        [Column("Name", TypeName = "NVARCHAR")]
        [StringLength(16)]
        public String Name { get; set; }

        #endregion Property

        #region Load

        public static List<ActionType> Load()
        {
            List<ActionType> entities = new List<ActionType>()
            {
                new ActionType() { Id = (Int16)ActionTypeEnum.Comment, Name = "Comment" },
                new ActionType() { Id = (Int16)ActionTypeEnum.Ack, Name = "Ack" },
                new ActionType() { Id = (Int16)ActionTypeEnum.Error, Name = "Error" },
                new ActionType() { Id = (Int16)ActionTypeEnum.Info, Name = "Info" },
                new ActionType() { Id = (Int16)ActionTypeEnum.Clear, Name = "Clear" },
            };

            return entities;
        }
        #endregion Load
    }

    public enum ActionTypeEnum : short
    {
        Comment = 1,
        Ack = 2,
        Error = 3,
        Info = 4,
        Clear = 5
    }
}
