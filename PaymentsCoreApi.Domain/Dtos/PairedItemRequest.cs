using System;
namespace PaymentsCoreApi.Domain.Dtos
{
	public class PairedItemRequest:BaseRequest
	{
		public string? PairCode { get; set; }
	}
}

