using PaymentsCoreApi.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsCoreApi.Logic.Interfaces
{
    public interface IUserAuthenticationManagement
    {
        Task<BaseResponse> ChangeUserPassword(PasswordChangeRequestDto request);
        Task<BaseResponse> CompleteUserPasswordReset(CompletePasswordResetRequestDto request);
        Task<UserAccountsDto> GetUserAccounts(UserAccountRequestDto request);
        Task<BaseResponse> InitiateUserPasswordReset(PasswordResetRequestDto request);
        Task<UserDetailsDto> UserLogin(LoginRequestDto request);
    }
}
