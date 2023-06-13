using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsCoreApi.Domain.Entities
{
    public class BaseEntity
    {
        [Column("created_by")]
        [StringLength(100)]
        public string? CreatedBy { get; set; }

        [Column("record_date")]
        public DateTime RecordDate { get; set; }

        [Column("last_updated_by")]
        [StringLength(100)]
        public string? LastUpdatedBy { get; set; }

        [Column("last_updated_date")]
        public DateTime? LastUpdatedDate { get; set; } = DateTime.Now;

        [Column("approved_by")]
        [StringLength(50)]
        public string? ApprovedBy { get; set; }

        [Column("approved_date")]
        public DateTime? ApprovedDate { get; set; }

        [Column("approved")]
        public bool Approved { get; set; }
    }
}
