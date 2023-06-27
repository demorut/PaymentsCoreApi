using Microsoft.AspNetCore.Mvc;
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
    }
}
