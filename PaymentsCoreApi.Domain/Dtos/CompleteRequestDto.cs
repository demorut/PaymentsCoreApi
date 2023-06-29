using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsCoreApi.Domain.Dtos
{
    public class CompleteRequestDto:BaseRequest
    {
        public string? RequestId { get; set; }
        public string? Otp { get; set; }
    }
}
