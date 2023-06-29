using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsCoreApi.Domain.Entities
{
    [Table("signup_requests")]
    public class SignUpRequest:BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("record_id")]
        public long RecordId { get; set; }

        [Column("first_name")]
        [StringLength(100)]
        public string? FirstName { get; set; }

        [Column("last_name")]
        [StringLength(100)]
        public string? LastName { get; set; }

        [Column("customer_type")]
        [StringLength(100)]
        public string? CustomerType { get; set; }

        [Column("status")]
        [StringLength(100)]
        public string? Status { get; set; }

        [Column("country_code")]
        [StringLength(100)]
        public string? CountryCode { get; set; }

        [Column("password")]
        [StringLength(200)]
        public string? Password { get; set; }

        [Column("phone_number")]
        [StringLength(100)]
        public string? PhoneNumber { get; set; }

        [Column("email")]
        [StringLength(100)]
        public string? Email { get; set; }

        [Column("otp")]
        [StringLength(100)]
        public string? Otp { get; set; }

        [Column("rand_code")]
        [StringLength(200)]
        public string? RandomCode { get; set; }
    }
}
