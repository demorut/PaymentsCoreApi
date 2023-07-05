using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentsCoreApi.Domain.Dtos;
using System;
using PaymentsCoreApi.Logic.Interfaces;
using PaymentsCoreApi.Logic.Implementations;

namespace PaymentsCoreApi.Controllers.v1
{
	public class AgentManagementController:BaseApiController
	{
		private readonly IAgentManagement _agentManagement;
		public AgentManagementController(IAgentManagement agentManagement) 
		{
			this._agentManagement = agentManagement;
		}

		[HttpPost]
        [Authorize]
        [ActionName("create_agent")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> InitiateAgentSignUp(AgentSignUpRequestDto request)
        {
            var response = new BaseResponse();
            try
            {
                var vaildation = request.IsValid();
                if (vaildation.Item1)
                {
                    response = await _agentManagement.InitiateAgentSignUp(request);
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
        [ActionName("aaprove_gent")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ApproveAgent(AgentApprovalRequestDto request)
        {
            var response = new BaseResponse();
            try
            {
                var vaildation = request.IsValid();
                if (vaildation.Item1)
                {
                    response = await _agentManagement.ApproveorRejectAgent(request);
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

