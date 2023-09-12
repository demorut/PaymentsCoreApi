using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentsCoreApi.Domain.Dtos;
using PaymentsCoreApi.Domain.Entities;
using PaymentsCoreApi.Logic.Implementations;
using PaymentsCoreApi.Logic.Interfaces;

namespace PaymentsCoreApi.Controllers.v1
{
    public class UserAuthenticationController : BaseApiController
    {
        private IUserAuthenticationManagement _userAuthenticationManagement;
        public UserAuthenticationController(IUserAuthenticationManagement userAuthenticationManagement)
        {
            this._userAuthenticationManagement = userAuthenticationManagement;
        }

        [HttpPost]
        [Authorize]
        [ActionName("user_Login")]
        [ProducesResponseType(typeof(UserDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserDetailsDto), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(UserDetailsDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UserLogin(LoginRequestDto request)
        {
            var response = new UserDetailsDto();
            try
            {
                response = await _userAuthenticationManagement.UserLogin(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = "100";
                response.ResponseMessage = "Sorry!, Service is currently unavailable please try again later ";
                return Ok(response);
            }
        }

        [HttpPost]
        [Authorize]
        [ActionName("change_user_password")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangeUserPassword(PasswordChangeRequestDto request)
        {
            var response = new BaseResponse();
            try
            {
                response = await _userAuthenticationManagement.ChangeUserPassword(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = "100";
                response.ResponseMessage = "Sorry!, Service is currently unavailable please try again later ";
                return Ok(response);
            }
        }

        [HttpPost]
        [Authorize]
        [ActionName("initiate_user_password_reset")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> InitiateUserPasswordReset(PasswordResetRequestDto request)
        {
            var response = new BaseResponse();
            try
            {
                var vaildation = request.IsValid();
                if (vaildation.Item1)
                {
                    response = await _userAuthenticationManagement.InitiateUserPasswordReset(request);
                }
                else
                {
                    response.ResponseCode = "100";
                    response.ResponseMessage = vaildation.Item2;
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = "100";
                response.ResponseMessage = "Sorry!, Service is currently unavailable please try again later ";
                return Ok(response);
            }
        }

        [HttpPost]
        [Authorize]
        [ActionName("complete_user_password_reset")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CompleteUserPasswordReset(CompletePasswordResetRequestDto request)
        {
            var response = new BaseResponse();
            try
            {
                response = await _userAuthenticationManagement.CompleteUserPasswordReset(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = "100";
                response.ResponseMessage = "Sorry!, Service is currently unavailable please try again later ";
                return Ok(response);
            }
        }

        [HttpPost]
        [Authorize]
        [ActionName("get_user_accounts")]
        [ProducesResponseType(typeof(UserAccountsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserAccountsDto), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(UserAccountsDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUserAccounts(UserAccountRequestDto request)
        {
            var response = new UserAccountsDto();
            try
            {
                response = await _userAuthenticationManagement.GetUserAccounts(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ResponseCode = "100";
                response.ResponseMessage = "Sorry!, Service is currently unavailable please try again later ";
                return Ok(response);
            }
        }
    }
}
