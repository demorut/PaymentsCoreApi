using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentsCoreApi.Domain.Dtos;
using PaymentsCoreApi.Logic.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PaymentsCoreApi.Controllers.v1
{
    public class VendorsManagementController :BaseApiController 
    {
        private IVendorManagement _vendorManagement;
        public VendorsManagementController(IVendorManagement vendorManagement)
        {
            this._vendorManagement = vendorManagement;
        }

        [HttpPost]
        [Authorize]
        [ActionName("add_vendor")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddVendor(AddVendorDto request)
        {
            try
            {
                var vaildation = request.IsValid();
                if (vaildation.Item1)
                {
                    var response = await _vendorManagement.AddVendor(request);
                    return Ok(response);
                }
                else
                    return BadRequest(new BaseResponse() { ResponseCode = "100", ResponseMessage = vaildation.Item2 });
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse() { ResponseCode = "100", ResponseMessage = ex.Message });
            }
        }
    }
}

