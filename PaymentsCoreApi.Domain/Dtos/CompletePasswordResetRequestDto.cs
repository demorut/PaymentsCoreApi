using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsCoreApi.Domain.Dtos
{
    public class CompletePasswordResetRequestDto: CompleteRequestDto
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? ConfirmedPassword { get; set; }
        public string? RequestReference { get; set; }
    }
}
