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

        [Column("Type", TypeName = "NVARCHAR")]
        [StringLength(16)]
        public String Type { get; set; }

        [Column("Message", TypeName = "NVARCHAR")]
        public String Message { get; set; }

        [Column("CustomerId", TypeName = "UNIQUEIDENTIFIER")]
        public Guid CustomerId { get; set; }

        [Column("UserId", TypeName = "UNIQUEIDENTIFIER")]
        public Guid UserId { get; set; }

        #endregion Property
    }
}
