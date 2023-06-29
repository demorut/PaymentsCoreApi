using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentsCoreApi.Domain.Entities
{
	[Table("Channels")]
	public class Channel:BaseEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column("RecordId")]
		public long RecordId { get; set; }

        [Column("ChannelKey")]
		[StringLength(100)]
		public string? ChannelKey { get; set; }

        [Column("ChannelSecretKey")]
        [StringLength(100)]
        public string? ChannelSecretKey { get; set; }
    }
}

