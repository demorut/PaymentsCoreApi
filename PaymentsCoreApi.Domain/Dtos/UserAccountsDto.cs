using System;
namespace PaymentsCoreApi.Domain.Dtos
{
	public class UserAccountsDto:BaseResponse
	{
        public List<AccountDetailsDto>? Accounts { get; set; }
    }
}

