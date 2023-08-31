using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentsCoreApi.Domain.Dtos;

namespace PaymentsCoreApi.Logic.Interfaces
{
    public interface IDataManagement
    {
        Task<QueryResponseDto> ExecuteNonQuery(QueryRequestDto request);
        Task<QueryResponseDto> ExecuteQuery(QueryRequestDto request);
        Task<PairedItemResponse> GetPariedItems(PairedItemRequest request);

        //Task<GeneralLegerDto> GetPendingGlRequest();
        Task<DataTable> InternalExecuteQuery(QueryRequestDto request);
    }
}
