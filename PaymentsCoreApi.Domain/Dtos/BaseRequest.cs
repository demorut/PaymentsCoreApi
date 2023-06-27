using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsCoreApi.Domain.Dtos
{
    public class BaseRequest
    {
        public string? Apikey { get; set; }
        public string? Channel { get; set; }
    }
}
