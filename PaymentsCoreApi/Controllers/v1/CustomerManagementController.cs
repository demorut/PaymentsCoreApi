using Microsoft.AspNetCore.Mvc;
using PaymentsCoreApi.Logic.Interfaces;

namespace PaymentsCoreApi.Controllers.v1
{
    public class CustomerManagementController : BaseApiController
    {
        private ICustomerManagement _customerManagement;
        public CustomerManagementController(ICustomerManagement customerManagement)
        {
            this._customerManagement = customerManagement;
        }
    }
}
