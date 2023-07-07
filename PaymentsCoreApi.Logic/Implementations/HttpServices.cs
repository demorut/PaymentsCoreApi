using System;
using PaymentsCoreApi.Logic.Interfaces;

namespace PaymentsCoreApi.Logic.Implementations
{
	public class HttpServices:IHttpServices
	{
		public HttpServices()
		{
		}

        public async Task<string> SendHttpRequest(string request, string token, string url)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                var handler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };
                var client = new HttpClient(handler);
                var webrequest = new HttpRequestMessage(HttpMethod.Post, url);
                webrequest.Headers.Add("Authorization", "Bearer " + token);
                var content = new StringContent(request, null, "application/json");
                webrequest.Content = content;
                response = await client.SendAsync(webrequest);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

