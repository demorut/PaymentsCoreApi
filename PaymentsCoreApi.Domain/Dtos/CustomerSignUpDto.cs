using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsCoreApi.Domain.Dtos
{
    public class CustomerSignUpDto:BaseRequest
    {
        public string ? FirstName { get; set; }
        public string ? LastName { get; set; }
        public string ? Email { get; set; }
        public string ? Password { get; set; }
        public string ? PhoneNumber { get; set; }
        public string ? CountryCode { get; set; }
        public string? ConfirmPassword { get; set; }

        public Tuple<bool, string> IsValid()
        {
            try
            {
                if (String.IsNullOrEmpty(this.FirstName))
                    return Tuple.Create(false, "Please Supply First name");
                if (String.IsNullOrEmpty(this.LastName))
                    return Tuple.Create(false, "Please Supply Last name");
                if (String.IsNullOrEmpty(this.PhoneNumber))
                    return Tuple.Create(false, "Please Supply Phone Number");
                if (String.IsNullOrEmpty(this.CountryCode))
                    return Tuple.Create(false, "Please select Country");
                if (String.IsNullOrEmpty(this.Password))
                    return Tuple.Create(false, "Please set your Password");
                if (!this.Password.Equals(this.ConfirmPassword))
                    return Tuple.Create(false, "Password provided does not match confirmation Password ");
                return Tuple.Create(true, "Successful");

            }
            catch (Exception ex)
            {
                return Tuple.Create(false, ex.Message);
            }
        }
    }
}
