using System;
namespace PaymentsCoreApi.Logic.Interfaces
{
	public interface IHttpServices
	{
        Task<string> SendHttpRequest(string request, string token, string url);

    }
}

