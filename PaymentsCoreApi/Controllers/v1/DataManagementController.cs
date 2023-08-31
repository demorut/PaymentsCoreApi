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
    public class DataManagementController : BaseApiController
    {
        private readonly IDataManagement _dataManagement;
        public DataManagementController(IDataManagement dataManagement)
        {
            this._dataManagement = dataManagement;
        }

        [HttpPost]
        [ActionName("execute_query")]
        [ProducesResponseType(typeof(QueryResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(QueryResponseDto), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(QueryResponseDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ExecuteQuery(QueryRequestDto request)
        {
            var response = new QueryResponseDto();
            try
            {
                var vaildation = request.IsValid();
                if (vaildation.Item1)
                {
                    var data = await _dataManagement.ExecuteQuery(request);
                    return Ok(data);
                }
                else
                    return Ok(new QueryResponseDto() { ResponseCode = "100", ResponseMessage = vaildation.Item2 });
            }
            catch (Exception ex)
            {
                return Ok(new QueryResponseDto() { ResponseCode = "100", ResponseMessage = ex.Message, Data = "" });
            }
        }

        [HttpPost]
        [ActionName("execute_non_query")]
        [ProducesResponseType(typeof(QueryResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(QueryResponseDto), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(QueryResponseDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ExecuteNonQuery(QueryRequestDto request)
        {
            var response = new QueryResponseDto();
            try
            {
                var vaildation = request.IsValid();
                if (vaildation.Item1)
                {
                    response = await _dataManagement.ExecuteNonQuery(request);
                    return Ok(response);
                }
                else
                    return Ok(new QueryResponseDto() { ResponseCode = "100", ResponseMessage = vaildation.Item2 });
            }
            catch (Exception ex)
            {
                return Ok(new QueryResponseDto() { ResponseCode = "100", ResponseMessage = ex.Message, Data = "" });
            }
        }

        [HttpPost]
        [ActionName("get_paired_items")]
        [ProducesResponseType(typeof(PairedItemResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(PairedItemResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(PairedItemResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPairedItems(PairedItemRequest request)
        {
            var response = new PairedItemResponse();
            try
            {
                    response = await _dataManagement.GetPariedItems(request);
                    return Ok(response);
            }
            catch (Exception ex)
            {
                return Ok(new PairedItemResponse() { ResponseCode = "100", ResponseMessage = ex.Message});
            }
        }
    }
}

