using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentsCoreApi.Domain.Dtos;
using PaymentsCoreApi.Logic.Implementations;
using PaymentsCoreApi.Logic.Interfaces;

namespace PaymentsCoreApi.Controllers.v1
{
    public class CustomerManagementController : BaseApiController
    {
        private ICustomerManagement _customerManagement;
        public CustomerManagementController(ICustomerManagement customerManagement)
        {
            this._customerManagement = customerManagement;
        }

        [HttpPost]
        [Authorize]
        [ActionName("initiate_customer_signup")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> InitiateCustomerSignUp(CustomerSignUpDto request)
        {
            var response = new BaseResponse();
            try
            {
                var vaildation = request.IsValid();
                if (vaildation.Item1)
                {
                    response = await _customerManagement.InitiateCustomerSignUp(request);
                    return Ok(response);
                }

                else
                    return Ok(new BaseResponse() { ResponseCode = "100", ResponseMessage = vaildation.Item2 });
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
        [ActionName("Complete_customer_signup")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CompleteCustomerSignUp(CompleteRequestDto request)
        {
            var response = new BaseResponse();
            try
            {
                response = await _customerManagement.CompleteCustomerSignUp(request);
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
