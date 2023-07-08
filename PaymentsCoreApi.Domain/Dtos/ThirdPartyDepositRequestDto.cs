using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentsCoreApi.Domain.Dtos
{
    public class ThirdPartyDepositRequestDto:BaseRequest
    {
        public string? CustomerId{get;set;}
        public string? Network{get;set;}
        public string? Amount{get;set;}
        public string? PhoneNumber{get;set;}
        public string? Narration { get; set; }
        public string? TransactionId { get; set; }
        public string? CurrencyCode { get; set; }
        public string? Email { get; set; }
        public string? CustomerAccount{get;set;}
        public Tuple<bool, string> IsValid()
        {
            try
            {
                if (String.IsNullOrEmpty(this.CustomerId))
                    return Tuple.Create(false, "Please Supply Customer Id");
                if (String.IsNullOrEmpty(this.CustomerAccount))
                    return Tuple.Create(false, "Please Supply Customer Account");
                if (String.IsNullOrEmpty(this.Network))
                    return Tuple.Create(false, "Please Supply Network");
                if (String.IsNullOrEmpty(this.Amount))
                    return Tuple.Create(false, "Please Supply Valid Transaction Amount");
                if (String.IsNullOrEmpty(this.PhoneNumber))
                    return Tuple.Create(false, "Please Enter Phone Number");
                if (String.IsNullOrEmpty(this.CurrencyCode))
                    return Tuple.Create(false, "Please Currency code");
                return Tuple.Create(true, "Successful");

            }
            catch (Exception ex)
            {
                return Tuple.Create(false, ex.Message);
            }
        }

    }
}