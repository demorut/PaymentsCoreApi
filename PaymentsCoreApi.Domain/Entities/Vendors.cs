using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentsCoreApi.Domain.Entities
{
    [Table("vendors")]
    public class Vendors : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("record_id")]
        public long RecordId { get; set; }

        
        [Column("vendor_code")]
        [StringLength(100)]
        public string? VendorCode { get; set; }

        [Column("vendor_name")]
        [StringLength(100)]
        public string? VendorName { get; set; }

        [Column("system_accountnumber")]
        [StringLength(100)]
        public string? SystemAccountNumber { get; set; }
    }
}

