using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentsCoreApi.Domain.Entities
{
    [Table("user_documents")]
    public class UserDocuments:BaseEntity
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("record_id")]
        public long RecordId { get; set; }

        [Key]
        [Column("user_id")]
        [StringLength(100)]
        public string? UserId { get; set; }
    }
}

