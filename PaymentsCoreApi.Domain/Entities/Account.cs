using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsCoreApi.Domain.Entities
{
    [Table("accounts")]
    public class Account:BaseEntity
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("record_id")]
        public long RecordId { get; set; }

        [Column("customer_id")]
        [StringLength(100)]
        public string? CustomerId { get; set; }

        [Key]
        [Column("account_number")]
        [StringLength(100)]
        public string? AccountNumber { get; set; }

        [Column("account_name")]
        [StringLength(100)]
        public string? AccountName { get; set; }

        [Column("account_type")]
        [StringLength(100)]
        public string? AccountType { get; set; }

        [Column("currency_code")]
        [StringLength(100)]
        public string? CurrencyCode { get; set; }

        [Column("account_status")]
        [StringLength(100)]
        public string? AccountStatus { get; set; }

        [Column("balance")]
        public double Balance { get; set; }
    }
}
