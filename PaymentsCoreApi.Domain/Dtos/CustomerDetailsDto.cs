using System;

namespace PaymentsCoreApi.Domain.Dtos
{
    public class CustomerDetailsDto
    {
        public string? RecordId { get; set; }
        public string? CustomerId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? CustomerType { get; set; }
        public bool CustomerStatus { get; set; }
        public string? CountryCode { get; set; }
        public string? UserId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
    }
}

