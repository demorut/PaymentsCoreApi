using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsCoreApi.Domain.Dtos
{
    public class BaseResponse
    {
        public string? ResponseCode { get; set; }
        public string? ResponseMessage { get; set; }
        public string? ResponseId { get; set; }
    }
}
