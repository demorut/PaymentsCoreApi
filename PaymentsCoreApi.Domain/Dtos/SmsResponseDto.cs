using System;
namespace PaymentsCoreApi.Domain.Dtos
{
	public class SmsResponseDto
	{
        public string? code { get; set; }
        public string? message { get; set; }
        public string? phoneno { get; set; }
        public string? datetime { get; set; }
        public string? status { get; set; }
        public int ap_id { get; set; }
    }
}

