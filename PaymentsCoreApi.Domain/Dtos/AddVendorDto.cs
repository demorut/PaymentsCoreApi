using System;
namespace PaymentsCoreApi.Domain.Dtos
{
	public class AddVendorDto:BaseRequest
	{
        public string? VendorCode { get; set; }
        public string? VendorName { get; set; }
        public string? CreatedBy { get; set; }
        public string? CountryCode { get; set; }
        public Tuple<bool, string> IsValid()
        {
            try
            {
                if (String.IsNullOrEmpty(this.VendorName))
                    return Tuple.Create(false, "Please Supply Vendor Name");
                if (String.IsNullOrEmpty(this.VendorCode))
                    return Tuple.Create(false, "Please supply Vendor code");
                if (String.IsNullOrEmpty(this.CountryCode))
                    return Tuple.Create(false, "Please supply CurrencyCode");
                if (String.IsNullOrEmpty(this.CreatedBy))
                    return Tuple.Create(false, "Please supply Created by field");
                return Tuple.Create(true, "Successful");

            }
            catch (Exception ex)
            {
                return Tuple.Create(false, ex.Message);
            }
        }
    }
}

