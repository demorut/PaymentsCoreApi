using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsCoreApi.Domain.Entities
{
    [Table("user_logins")]
    public class UserLogins:BaseEntity
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("record_id")]
        public long RecordId { get; set; }

        [Column("customer_id")]
        [StringLength(200)]
        public string CustomerId { get; set; }

        [Column("Username")]
        [StringLength(200)]
        public string Username { get; set; }

        [Column("password")]
        [StringLength(200)]
        public string Password { get; set; }

        [Column("rand_code")]
        [StringLength(200)]
        public string RandomCode { get; set; }

        [Column("reset")]
        public bool Reset { get; set; }

        [Column("active")]
        public bool Active { get; set; }

        [Column("last_password_change_date")]
        public DateTime LastPasswordChangeDate { get; set; } = DateTime.Now;

        [Column("last_login_date")]
        public DateTime LastLoginDate { get; set; }=DateTime.Now;

        [Column("login_attempts")]
        public int LoginAttempts { get; set; }
    }
}
