using System;

namespace PaymentsCoreApi.Domain.Dtos
{
	public class AgentDetailsDto
	{
        public string? RecordId { get; set; }
        public string? AgentId { get; set; }
        public string? AgentName { get; set; }
        public string? ContactName { get; set; }
        public string? AgentType { get; set; }
        public bool AgentStatus { get; set; }
        public string? CountryCode { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? IdType { get; set; }
        public string? IdNumber { get; set; }
    }
}

