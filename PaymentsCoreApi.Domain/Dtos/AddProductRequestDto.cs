using System;
namespace PaymentsCoreApi.Domain.Dtos
{
	public class AddProductRequestDto:BaseRequest
	{
		public ProductDto? Product { get; set; }


        public Tuple<bool, string> IsValid()
        {
            try
            {
                if (String.IsNullOrEmpty(this.Product.ProductCode))
                    return Tuple.Create(false, "Please Supply Product Code");
                if (String.IsNullOrEmpty(this.Product.ProductName))
                    return Tuple.Create(false, "Please Supply Poduct name");

                return Tuple.Create(true, "Successful");

            }
            catch (Exception ex)
            {
                return Tuple.Create(false, ex.Message);
            }
        }
    }
}

