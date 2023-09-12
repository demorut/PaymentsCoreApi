using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsCoreApi.Domain.Dtos
{
    public class UserDetailsDto:BaseResponse
    {
        public bool ResetPassword { get; set; }
        public string? UserType { get; set; }
        public string? UserId { get; set; }
        public AgentDetailsDto? AgentDetails { get; set; }
        public CustomerDetailsDto? CustomerDetails { get; set; }
        public List<AccountDetailsDto>? Accounts { get; set; }
    }
}
