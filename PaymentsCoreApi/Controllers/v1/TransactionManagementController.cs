using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentsCoreApi.Domain.Dtos;
using PaymentsCoreApi.Logic.Interfaces;

namespace PaymentsCoreApi.Controllers.v1
{
    public class TransactionManagementController:BaseApiController
    {
        private readonly ITransactionManagement _transactionManagement;
        public TransactionManagementController(ITransactionManagement transactionManagement){
            this._transactionManagement=transactionManagement;
        }

        [HttpPost]
        [Authorize]
        [ActionName("make_thirdparty_deposit")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> MakeThridPartyDeposit(ThirdPartyDepositRequestDto request)
        {
            var response = new BaseResponse();
            try
            {
                var vaildation = request.IsValid();
                if (vaildation.Item1)
                {
                    response = await _transactionManagement.MakeThridPartyDeposit(request);
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
        [ActionName("make_bill_payment")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> MakeBillPayment(ThirdPartyDepositRequestDto request)
        {
            var response = new BaseResponse();
            try
            {
                var vaildation = request.IsValid();
                if (vaildation.Item1)
                {
                    response = await _transactionManagement.MakeThridPartyDeposit(request);
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
        [ActionName("make_Internal_transfer")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> MakeInternalTransfer(ThirdPartyDepositRequestDto request)
        {
            var response = new BaseResponse();
            try
            {
                var vaildation = request.IsValid();
                if (vaildation.Item1)
                {
                    response = await _transactionManagement.MakeThridPartyDeposit(request);
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
    }
}