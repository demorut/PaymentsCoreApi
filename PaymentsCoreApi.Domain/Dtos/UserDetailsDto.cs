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
    }
}
