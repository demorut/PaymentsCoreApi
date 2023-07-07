using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentsCoreApi.Domain.Entities
{
    [Table("products")]
	public class Products:BaseEntity
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("record_id")]
        public long RecordId { get; set; }

        [Column("product_code")]
        [StringLength(100)]
        public string? ProductCode{ get; set; }

        [Column("product_name")]
        [StringLength(100)]
        public string? ProductName{ get; set; }

        [Column("suspense_account")]
        [StringLength(100)]
        public string? SuspenseAccount { get; set; }

        [Column("commission_account")]
        [StringLength(100)]
        public string? CommissionAccount { get; set; }

        [Column("active")]
        public bool Active { get; set; }

        [Column("country_code")]
        [StringLength(100)]
        public string? CountryCode { get; set; }
    }
}

