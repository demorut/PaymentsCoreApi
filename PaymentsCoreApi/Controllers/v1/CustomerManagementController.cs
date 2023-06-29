﻿using Microsoft.AspNetCore.Mvc;
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
        [ActionName("initiate_customer_signup")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> InitiateCustomerSignUp(CustomerSignUpDto request)
        {
            var response = new BaseResponse();
            try
            {
                response = await _customerManagement.InitiateCustomerSignUp(request);
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
