using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentsCoreApi.Domain.Entities
{
    [Table("agents_signup_requests")]
    public class AgentSignUpRequest:BaseEntity
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("record_id")]
        public long RecordId { get; set; }

        [Column("agent_id")]
        [StringLength(100)]
        public string? AgentId { get; set; }

        [Column("agent_name")]
        [StringLength(100)]
        public string? AgentName { get; set; }

        [Column("contact_name")]
        [StringLength(100)]
        public string? ContactName { get; set; }

        [Column("agent_type")]
        [StringLength(100)]
        public string? AgentType { get; set; }

        [Column("agent_status")]
        [StringLength(25)]
        public string? AgentStatus { get; set; }

        [Column("country_code")]
        [StringLength(100)]
        public string? CountryCode { get; set; }

        [Column("phone_number")]
        [StringLength(100)]
        public string? PhoneNumber { get; set; }

        [Column("email")]
        [StringLength(100)]
        public string? Email { get; set; }

        [Column("city")]
        [StringLength(100)]
        public string? City { get; set; }

        [Column("street")]
        [StringLength(100)]
        public string? Street { get; set; }

        [Column("id_type")]
        [StringLength(100)]
        public string? IdType { get; set; }

        [Column("id_number")]
        [StringLength(100)]
        public string? IdNumber { get; set; }

        [Column("documents")]
        public string? Documents { get; set; }

        [Column("comment")]
        [StringLength(200)]
        public string? Comment { get; set; }
    }
}

