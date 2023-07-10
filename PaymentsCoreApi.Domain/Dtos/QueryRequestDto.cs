using System;
using System.Collections;

namespace PaymentsCoreApi.Domain.Dtos
{
	public class QueryRequestDto:BaseRequest
	{
        public string? StoredProcedure { get; set; }
        public Hashtable? Parameters { get; set; }

        public Tuple<bool, string> IsValid()
        {
            try
            {
                if (String.IsNullOrEmpty(this.StoredProcedure))
                    return Tuple.Create(false, "Please supply a Stored Procedure");
                if (String.IsNullOrEmpty(this.Signature))
                    return Tuple.Create(false, "Please supply a signature");
                else
                    return Tuple.Create(true, "Success");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

