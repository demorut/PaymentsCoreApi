using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsCoreApi.Domain.Entities
{
    [Table("general_ledger")]
    public class GeneralLedger
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("record_id")]
        public long RecordId { get; set; }

        [Column("account_number")]
        [StringLength(100)]
        public string? AccountNumber { get; set; }

        [Column("transaction_type")]
        [StringLength(100)]
        public string? TransactionType { get; set; }

        [Column("debit_amount")]
        public double DebitAmount { get; set; }

        [Column("credit_amount")]
        public double CreditAmount { get; set; }

        [Column("transaction_date")]
        public DateTime TransactionDate { get; set; }

        [Column("partner_id")]
        [StringLength(100)]
        public string? PartnerId { get; set; }

        [Column("system_id")]
        [StringLength(100)]
        public string? SystemId { get; set; }

        [Column("transaction_header_id")]
        [StringLength(100)]
        public string? TransactionHeaderId { get; set; }

        [Column("naration")]
        [StringLength(100)]
        public string? Naration { get; set; }

        [Column("channel")]
        [StringLength(100)]
        public string? Channel { get; set; }

        [Column("transaction_category")]
        [StringLength(100)]
        public string? TransactionCategory { get; set; }

        [Column("balance")]
        public double Balance { get; set; }

        [Column("customer_id")]
        [StringLength(100)]
        public string? CustomerId { get; set; }
    }
}
