using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsCoreApi.Domain.Entities
{
    [Table("transaction_header")]
    public class TransactionHeader:BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("record_id")]
        public long RecordId { get; set; }

        [Column("from_account")]
        [StringLength(100)]
        public string? FromAccount { get; set; }

        [Column("from_customer_id")]
        [StringLength(100)]
        public string? FromCustomerId { get; set; }

        [Column("to_account")]
        [StringLength(100)]
        public string? ToAccount { get; set; }

        [Column("to_customer_id")]
        [StringLength(100)]
        public string? ToCustomerId { get; set; }

        [Column("transaction_amount")]
        public double TransactionAmount { get; set; }

        [Column("transaction_header_id")]
        [StringLength(100)]
        public string? TransactionHeaderId { get; set; }

        [Column("partner_id")]
        [StringLength(100)]
        public string? PartnerId { get; set; }

        [Column("channel")]
        [StringLength(100)]
        public string? Channel { get; set; }

        [Column("status")]
        [StringLength(50)]
        public string? Status { get; set; }

        [Column("ledger_id")]
        [StringLength(50)]
        public string? ledger_id { get; set; }

        [Column("product_type")]
        [StringLength(50)]
        public string? ProductCode { get; set; }

        [Column("transaction_type")]
        [StringLength(50)]
        public string? TransactionType { get; set; }

        [Column("partner")]
        [StringLength(50)]
        public string? Partner { get; set; }
    }
}
