using System;
namespace PaymentsCoreApi.Domain.Dtos
{
	public class ProductDto
	{
        public string? ProductCode { get; set; }
        public string? ProductName { get; set; }
        public bool Active { get; set; }
        public string? CountryCode { get; set; }
    }
}

