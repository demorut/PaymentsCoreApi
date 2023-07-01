using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsCoreApi.Logic.Interfaces
{
    public interface ICommonLogic
    {
        string GenerateOTP();
        string GetAccountNumber(string? customerId);
        bool IsValidCredentails(string? signature, string inputstring);
        Task SendEmailNotification(string recipientEmail, string emailmessage, string signUpEmailSubject);
        Task SendSmsNotification(string phonenumber, string smsmessage);
    }
}
