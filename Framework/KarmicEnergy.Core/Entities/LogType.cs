using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("LogTypes", Schema = "dbo")]
    public class LogType : BaseEntity
    {
        #region Constructor
        public LogType()
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

        #region Functions

        public static List<LogType> Load()
        {
            List<LogType> entities = new List<LogType>()
            {
                new LogType() { Id = (Int16)LogTypeEnum.Info, Name = "Info" },
                new LogType() { Id = (Int16)LogTypeEnum.Warning, Name = "Warning" },
                new LogType() { Id = (Int16)LogTypeEnum.Error, Name = "Error" },
            };

            return entities;
        }

        public void Update(LogType entity)
        {
            this.Name = entity.Name;          

            this.CreatedDate = entity.CreatedDate;
            this.LastModifiedDate = entity.LastModifiedDate;
            this.DeletedDate = entity.DeletedDate;
        }
        #endregion Functions
    }

    public enum LogTypeEnum : short
    {
        Info = 1,
        Warning = 2,
        Error = 3
    }
}
