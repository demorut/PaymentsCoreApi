using System;

namespace PaymentsCoreApi.Domain.Dtos
{
	public class AccountDetailsDto
	{
        public string? RecordId { get; set; }
        public string? CustomerId { get; set; }
        public string? AccountNumber { get; set; }
        public string? AccountName { get; set; }
        public string? AccountType { get; set; }
        public string? CurrencyCode { get; set; }
        public string? AccountStatus { get; set; }
        public double Balance { get; set; }
    }
}

