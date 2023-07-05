using System;
using PaymentsCoreApi.Domain.Dtos;

namespace PaymentsCoreApi.Logic.Interfaces
{
    public interface IAgentManagement
    {
        Task<BaseResponse> ApproveorRejectAgent(AgentApprovalRequestDto request);
        Task<BaseResponse> InitiateAgentSignUp(AgentSignUpRequestDto request);
    }
}

