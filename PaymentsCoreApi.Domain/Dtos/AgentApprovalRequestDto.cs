using System;
namespace PaymentsCoreApi.Domain.Dtos
{
	public class AgentApprovalRequestDto : BaseRequest
	{
		public string? ApprovalStatus { get; set; }
		public string? AgentId { get; set; }
        public string? Comment { get; set; }
        public string? ApprovedBy { get; set; }

        public Tuple<bool, string> IsValid()
        {
            try
            {
                if (String.IsNullOrEmpty(this.AgentId))
                    return Tuple.Create(false, "Please Supply Agent Code");
                if (String.IsNullOrEmpty(this.ApprovalStatus))
                    return Tuple.Create(false, "Please Supply Approval Status");
                if (String.IsNullOrEmpty(this.Signature))
                    return Tuple.Create(false, "Please Supply Api Signature");
                if (String.IsNullOrEmpty(this.RequestTimestamp))
                    return Tuple.Create(false, "Please select Country");
                if (String.IsNullOrEmpty(this.ApprovedBy))
                    return Tuple.Create(false, "Approved by value is required");

                return Tuple.Create(true, "Successful");
            }
            catch (Exception ex)
            {
                return Tuple.Create(false, ex.Message);
            }
        }
    }
}

