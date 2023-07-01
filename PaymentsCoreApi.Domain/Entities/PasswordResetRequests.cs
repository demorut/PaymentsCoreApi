using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsCoreApi.Domain.Entities
{
    [Table("password_reset_requests")]
    public class PasswordResetRequests:BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("record_id")]
        public long RecordId { get; set; }

        [Column("status")]
        [StringLength(100)]
        public string? Status { get; set; }

        [Column("username")]
        [StringLength(100)]
        public string? Username { get; set; }

        [Column("otp")]
        [StringLength(100)]
        public string? Otp { get; set; }
    }
}
