using System;
namespace PaymentsCoreApi.Domain.Dtos
{
	public class UserAccountRequestDto:BaseRequest
	{
		public string? CustomerId { get; set; }
	}
}

