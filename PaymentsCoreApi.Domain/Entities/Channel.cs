using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentsCoreApi.Domain.Entities
{
	[Table("channels")]
	public class Channel:BaseEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column("record_id")]
		public long RecordId { get; set; }

        [Column("channel_key")]
		[StringLength(100)]
		public string? ChannelKey { get; set; }

        [Column("channel_secretKey")]
        [StringLength(100)]
        public string? ChannelSecretKey { get; set; }
    }
}

