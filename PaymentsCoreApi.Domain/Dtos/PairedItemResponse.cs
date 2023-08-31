using System;
namespace PaymentsCoreApi.Domain.Dtos
{
	public class PairedItemResponse:BaseResponse
	{
		public List<PairedItem>? PairedItems { get; set; }
	}
	public class PairedItem 
    { 
		public string? ItemName { get; set; }
		public string? ItemValue { get; set; }
    }
}

