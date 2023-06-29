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
        bool IsValidCredentails(string? signature, string inputstring);
        Task SendEmailNotification(string email, string emailmessage, string signUpEmailSubject);
        Task SendSmsNotification(string phonenumber, string smsmessage);
    }
}
