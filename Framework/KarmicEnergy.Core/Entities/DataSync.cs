using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmicEnergy.Core.Entities
{
    [Table("DataSyncs", Schema = "dbo")]
    public class DataSync : BaseEntity
    {
        #region Constructor
        public DataSync()
        {
            this.Id = Guid.NewGuid();
        }
        #endregion Constructor

        #region Property

        [Key, Column("Id", Order = 1, TypeName = "UNIQUEIDENTIFIER")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
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
