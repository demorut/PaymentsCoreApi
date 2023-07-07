using PaymentsCoreApi.Data.Contexts;
using PaymentsCoreApi.Logic.Helpers;
using PaymentsCoreApi.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Diagnostics.Metrics;
using static System.Net.Mime.MediaTypeNames;
using PaymentsCoreApi.Domain.Constants;
using System.Text.Json;
using PaymentsCoreApi.Domain.Dtos;

namespace PaymentsCoreApi.Logic.Implementations
{
    public class CommonLogic:ICommonLogic
    {
        private readonly IHttpServices _httpServices;
        private readonly DataBaseContext _dataBaseContext;
        private readonly string smtphost = "smtp.gmail.com";
        private readonly int smtpport = 587;
        private readonly string senderEmail = "emorutdeogratius@gmail.com";
        private readonly string senderPassword = "KevinDero@0825";
        private readonly string smsUrl ="";
        /*"https://esbinternal.nssfug.org/services/RegSMSSendService"*/
        public CommonLogic(DataBaseContext dataBaseContext, IHttpServices httpServices)
        {
            _dataBaseContext = dataBaseContext;
            _httpServices = httpServices;
        }

        public bool IsValidCredentails(string signature, string inputstring)
        {
            try
            {
                if (!String.IsNullOrEmpty(signature) && !String.IsNullOrEmpty(inputstring))
                {
                    var hash = Helper.GenerateApiSignature(inputstring);
                    if (signature.Equals(hash))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string GenerateOTP()
        {
            // Create a new random number generator.
            Random random = new Random();
            // Generate a 5 digit random number.
            int otp = random.Next(100000, 999999);
            // Return the OTP as a string.
            return otp.ToString();
        }
        public async Task SendEmailNotification(string recipientEmail, string emailmessage, string signUpEmailSubject)
        {
            try
            {
                // Set up the SMTP client
                SmtpClient smtpClient = new SmtpClient(smtphost, smtpport);
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
                MailAddress fromAddress =new MailAddress(senderEmail,"Wallet-App");
                MailAddress toAddress = new MailAddress(recipientEmail);
                // Create a MailMessage object
                MailMessage mailMessage = new MailMessage(fromAddress, toAddress);
                mailMessage.Subject = signUpEmailSubject;
                mailMessage.Body = emailmessage;
                // Send the email
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                // do nothing
            }
        }

        public async Task SendSmsNotification(string phonenumber, string smsmessage)
        {
            try
            {
                var Smsobject = new SmsObjectDto()
                {
                    content = smsmessage,
                    destination = Helper.FormatPhoneNumber(phonenumber)
                };
                var response = await _httpServices.SendHttpRequest(JsonSerializer.Serialize(Smsobject), "",smsUrl);
                var resp = JsonSerializer.Deserialize<SmsResponseDto>(response);
            }
            catch (Exception)
            {
            }
        }
        public string GetAccountNumber(string customerId)
        {
            var timeNow = DateTime.Now;
            string accountnumber = timeNow.ToString("yy") + customerId.Substring(customerId.Length - 5)+ timeNow.ToString("dd") + timeNow.ToString("HH") + timeNow.ToString("mm")+ timeNow.ToString("fff");
            return accountnumber;
        }
    }
}
