using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsCoreApi.Domain.Dtos
{
    public class PasswordResetRequestDto:BaseRequest
    {
        public string? Username { get; set; }
        public string? RequestReference { get; set; }

        public Tuple<bool, string> IsValid()
        {
            try
            {
                if (String.IsNullOrEmpty(this.Username))
                    return Tuple.Create(false, "Please Supply UserName");
                if (String.IsNullOrEmpty(this.RequestReference))
                    return Tuple.Create(false, "Failed to process request try again later");
                return Tuple.Create(true, "Successful");

            }
            catch (Exception ex)
            {
                return Tuple.Create(false, ex.Message);
            }
        }
    }
}
