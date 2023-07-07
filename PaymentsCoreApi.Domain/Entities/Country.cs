using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsCoreApi.Domain.Entities
{
    [Table("country")]
    public class Country:BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("record_id")]
        public long RecordId { get; set; }
       
        [Column("country_code")]
        [StringLength(100)]
        public string? CountryCode{ get; set; }

        [Column("country_name")]
        [StringLength(100)]
        public string? CountryName { get; set; }

        [Column("currency_code")]
        [StringLength(100)]
        public string? CurrencyCode { get; set; }

        [Column("currency")]
        [StringLength(100)]
        public string? Currency { get; set; }
    }
}
