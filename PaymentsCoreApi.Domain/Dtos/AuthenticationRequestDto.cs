using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsCoreApi.Domain.Dtos
{
    public class AuthenticationRequestDto:BaseRequest
    {
        public string? apikey { get; set; }
    }
}
