using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsCoreApi.Domain.Entities
{
    [Table("customers")]
    public class Customers:BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("record_id")]
        public long RecordId { get; set; }

        [Key]
        [Column("customer_id")]
        [StringLength(100)]
        public string? CustomerId { get; set; }

        [Column("first_name")]
        [StringLength(100)]
        public string? FirstName { get; set; }

        [Column("last_name")]
        [StringLength(100)]
        public string? LastName { get; set; }

        [Column("customer_type")]
        [StringLength(100)]
        public string? CustomerType { get; set; }

        [Column("customer_status")]
        public bool CustomerStatus { get; set; }

        [Column("country_code")]
        [StringLength(100)]
        public string? CountryCode { get; set; }

        [Column("user_id")]
        [StringLength(100)]
        public string? UserId { get; set; }

        [Column("phone_number")]
        [StringLength(100)]
        public string? PhoneNumber { get; set; }

        [Column("email")]
        [StringLength(100)]
        public string? Email { get; set; }
    }
}
