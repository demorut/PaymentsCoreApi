using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentsCoreApi.Domain.Dtos
{
	public class AgentSignUpRequestDto:BaseRequest
	{
        public string? AgentName { get; set; }
        public string? ContactName { get; set; }
        public string? AgentType { get; set; }
        public string? CountryCode { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? IdType { get; set; }
        public string? IdNumber { get; set; }
        public List<UserDocumentDto>? Documents { get; set; }
        public string? CreatedBy { get; set; } 

        public Tuple<bool, string> IsValid()
        {
            try
            {
                if (String.IsNullOrEmpty(this.AgentName))
                    return Tuple.Create(false, "Please Supply First name");
                if (String.IsNullOrEmpty(this.ContactName))
                    return Tuple.Create(false, "Please Supply Last name");
                if (String.IsNullOrEmpty(this.PhoneNumber))
                    return Tuple.Create(false, "Please Supply Phone Number");
                if (String.IsNullOrEmpty(this.CountryCode))
                    return Tuple.Create(false, "Please select Country");
                if (String.IsNullOrEmpty(this.AgentType))
                    return Tuple.Create(false, "Please set your Password");
                if (String.IsNullOrEmpty(this.City))
                    return Tuple.Create(false, "Please supply the Agent's city");
                if (String.IsNullOrEmpty(this.Street))
                    return Tuple.Create(false, "Please supply the Agent's street");
                if (String.IsNullOrEmpty(this.IdType))
                    return Tuple.Create(false, "Please supply owner's Id Number");
                if (String.IsNullOrEmpty(this.IdNumber))
                    return Tuple.Create(false, "Please supplyowner's id number");
                if (this.Documents==null)
                    return Tuple.Create(false, "Please upload all the required");
                if (String.IsNullOrEmpty(this.CreatedBy))
                    return Tuple.Create(false, "Please request creator");

                return Tuple.Create(true, "Successful");

            }
            catch (Exception ex)
            {
                return Tuple.Create(false, ex.Message);
            }
        }
    }
}

