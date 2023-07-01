using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsCoreApi.Domain.Dtos
{
    public class LoginRequestDto:BaseRequest
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
