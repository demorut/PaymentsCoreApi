using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentsCoreApi.Domain.Dtos;
using PaymentsCoreApi.Logic.Implementations;
using PaymentsCoreApi.Logic.Interfaces;

namespace PaymentsCoreApi.Controllers.v1
{
   
    public class ProductsManagementController : BaseApiController
    {
        private IProductManagement _productManagement;
        public ProductsManagementController(IProductManagement productManagement)
        {
            this._productManagement = productManagement;
        }

        [HttpPost]
        //[Authorize]
        [ActionName("add_product")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddProduct(AddProductRequestDto request)
        {
            var response = new BaseResponse();
            try
            {
                var vaildation = request.IsValid();
                if (vaildation.Item1)
                {
                    response = await _productManagement.AddProductDetails(request);
                    return Ok(response);
                }
                else
                    return Ok(new BaseResponse() { ResponseCode = "100", ResponseMessage = vaildation.Item2 });

            }
            catch (Exception ex)
            {
                response.ResponseCode = "100";
                response.ResponseMessage = "Sorry!, Service is currently unavailable please try again later ";
                return Ok(response);
            }
        }

        [HttpPost]
        //[Authorize]
        [ActionName("get_products")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProducts(AddProductRequestDto request)
        {
            var response = new BaseResponse();
            try
            {
                var vaildation = request.IsValid();
                if (vaildation.Item1)
                {
                    response = await _productManagement.AddProductDetails(request);
                    return Ok(response);
                }
                else
                    return Ok(new BaseResponse() { ResponseCode = "100", ResponseMessage = vaildation.Item2 });

            }
            catch (Exception ex)
            {
                response.ResponseCode = "100";
                response.ResponseMessage = "Sorry!, Service is currently unavailable please try again later ";
                return Ok(response);
            }
        }
    }
}
