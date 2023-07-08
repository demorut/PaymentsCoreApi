using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentsCoreApi.Domain.Entities
{
    [Table("thirdparty_deposits")]
    public class ThirdPartyDeposits:BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("record_id")]
        public long RecordId { get; set; }

        [Column("customer_id")]
        [StringLength(100)]
        public string? CustomerId { get; set; }

        [Column("customer_account")]
        [StringLength(100)]
        public string? CustomerAccount { get; set; }

        [Column("transaction_amount")]
        public double TransactionAmount { get; set; }

        [Column("request_id")]
        [StringLength(100)]
        public string? RequestId { get; set; }

        [Column("network")]
        [StringLength(50)]
        public string? Network { get; set; }

        [Column("thirdparty_id")]
        [StringLength(100)]
        public string? ThirdPartyId { get; set; }

        [Column("channel")]
        [StringLength(100)]
        public string? Channel { get; set; }

        [Column("status")]
        [StringLength(50)]
        public string? Status { get; set; }

        [Column("SystemId")]
        [StringLength(50)]
        public string? SystemId { get; set; }
    }
}