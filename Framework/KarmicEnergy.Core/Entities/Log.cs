using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("Logs", Schema = "dbo")]
    public class Log : BaseEntity
    {
        #region Constructor
        public Log()
        {

        }
        #endregion Constructor

        #region Property

        [Key, Column("Id", Order = 1, TypeName = "UNIQUEIDENTIFIER")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("Message", TypeName = "NVARCHAR")]
        public String Message { get; set; }

        [Column("CustomerId", TypeName = "UNIQUEIDENTIFIER")]
        public Guid? CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        [Column("UserId", TypeName = "UNIQUEIDENTIFIER")]
        public Guid? UserId { get; set; }

        //[ForeignKey("UserId")]
        //public virtual User User { get; set; }

        [Column("SiteId", TypeName = "UNIQUEIDENTIFIER")]
        public Guid? SiteId { get; set; }

        [ForeignKey("SiteId")]
        public virtual Site Site { get; set; }

        #endregion Property

        #region LogType

        [Column("LogTypeId", TypeName = "SMALLINT")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} cannot be null or empty")]
        public Int16 LogTypeId { get; set; } = (Int16)LogTypeEnum.Info;

        [ForeignKey("LogTypeId")]
        public virtual LogType LogType { get; set; }

        #endregion LogType

        #region Functions

        public void Update(Log entity)
        {
            this.CustomerId = entity.CustomerId;
            this.SiteId = entity.SiteId;
            this.UserId = entity.UserId;
            this.Message = entity.Message;
            this.LogTypeId = entity.LogTypeId;

            this.CreatedDate = entity.CreatedDate;
            this.LastModifiedDate = entity.LastModifiedDate;
            this.DeletedDate = entity.DeletedDate;
        }
        #endregion Functions
    }
}
