using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentsCoreApi.Domain.Entities
{
    [Table("user_documents")]
    public class UserDocuments:BaseEntity
	{
        [Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("record_id")]
        public long RecordId { get; set; }
        
        [Column("user_id")]
        [StringLength(100)]
        public string? UserId { get; set; }

        [Column("document_type")]
        [StringLength(100)]
        public string? DocumentType { get; set; }
  
        [Column("document_name")]
        [StringLength(100)]
        public string? DocumentName { get; set; }

        [Column("document_extension")]
        [StringLength(100)]
        public string? DocumentExtension { get; set; }

        [Column("document_path")]
        [StringLength(150)]
        public string? DocumentPath { get; set; }


    }
}

