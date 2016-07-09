using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("DataSyncs", Schema = "dbo")]
    public class DataSync : BaseEntity
    {
        #region Property

        [Key, Column("Id", Order = 1, TypeName = "UNIQUEIDENTIFIER")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("SiteId", TypeName = "UNIQUEIDENTIFIER")]
        [Required]
        public Guid SiteId { get; set; }

        [Column("SyncDate", TypeName = "DATETIME")]
        [Required]
        public DateTime SyncDate { get; set; }
       
        #endregion Property        
    }
}
