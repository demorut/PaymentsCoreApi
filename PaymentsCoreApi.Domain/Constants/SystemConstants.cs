using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsCoreApi.Domain.Constants
{
    public class SystemConstants
    {
        public const string Pending = "PENDING";
        public const string Approved = "APPROVED";
        public const string Rejected = "REJECTED";
        public const string Customer = "CUSTOMER";
        public const string SignUpEmailSubject = "Customer SignUp";
        public const string CustomerAccountType = "NORMAL";
        public const string SuspenseAccountType = "SUSPENSE";
        public const string CommissionAccountcode = "COMM";
        public const string SuspenseAccountcode = "GL";
        public const int PasswordExpiryDays = 365;
        public const string GetPairedItemSp = "GetPairedItems";
    }
}
