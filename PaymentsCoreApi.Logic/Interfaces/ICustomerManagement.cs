using PaymentsCoreApi.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsCoreApi.Logic.Interfaces
{
    public interface ICustomerManagement
    {
        Task<BaseResponse> InitiateCustomerSignUp(CustomerSignUpDto request);
    }
}
