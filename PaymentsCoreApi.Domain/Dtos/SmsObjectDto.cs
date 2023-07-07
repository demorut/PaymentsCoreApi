using System;
namespace PaymentsCoreApi.Domain.Dtos
{
	public class SmsObjectDto
	{
        public string? content { get; set; }
        public string? destination { get; set; }
    }
}

