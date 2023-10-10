using Microsoft.AspNetCore.Mvc;
using PaymentsCoreApi.Domain.Dtos;
using PaymentsCoreApi.Logic.Interfaces;

namespace PaymentsCoreApi.Controllers
{
    public class AuthenticationController : BaseApiController
    {
        private IAuthenticationManagement _authenticationManagment;
        public AuthenticationController(IAuthenticationManagement authenticationManagment)
        {
            this._authenticationManagment = authenticationManagment;
        }

        [HttpPost]
        [ActionName("get_auth_Token")]
        [ProducesResponseType(typeof(AuthenticationResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AuthenticationResponseDto), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(AuthenticationResponseDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAuthorizationToken(AuthenticationRequestDto request)
        {
            var response = new AuthenticationResponseDto();
            try
            {
                response = await _authenticationManagment.AuthenticateReuest(request);
                if (!String.IsNullOrEmpty(response.access_token))
                    return Ok(response);
                else
                    return Unauthorized(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                response.access_token = "";
                response.expires_in = "";
                return Unauthorized(response);
            }
        }
    }
}
