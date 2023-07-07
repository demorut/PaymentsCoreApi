using System;
using System.Data;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace PaymentsCoreApi.Logic.Helpers
{
    public static class Helper
    {
        public static List<T> ExtractList<T>(DataTable dt)
        {
            try
            {
                List<T> data = new List<T>();
                foreach (DataRow row in dt.Rows)
                {
                    T item = GetItem<T>(row);
                    data.Add(item);
                }
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static T ExtractObject<T>(DataRow drow)
        {
            try
            {
                T item = GetItem<T>(drow);
                return item;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName && dr[column] != DBNull.Value)
                        pro.SetValue(obj, dr[column.ColumnName].ToString(), null);
                    else
                        continue;
                }
            }
            return obj;
        }
        public static bool IsNumeric(string? value)
        {
            double number;
            return double.TryParse(value, out number);
        }
        public static string GenerateApiSignature(string input)
        {
            try
            {
                using (SHA512 sha512 = SHA512.Create())
                {
                    byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                    byte[] hashBytes = sha512.ComputeHash(inputBytes);
                    string hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                    return hash;
                }
            }
            catch (Exception)
            {
                return "";
            }

        }

        public static string? GetPhoneNumberNetworkcode(string? phoneNumber)
        {
            try
            {
                string formatedphone = FormatPhoneNumberNonCoded(phoneNumber);
                return formatedphone.Substring(0, 2);
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string FormatPhoneNumberNonCoded(string? phonenumber)
        {
            if (phonenumber.StartsWith("256") && phonenumber.Length == 12)
            {
                return phonenumber.Remove(0, 3);
            }
            else if (phonenumber.Length == 10 && phonenumber.StartsWith("0"))
            {
                return phonenumber.Remove(0, 1);
            }
            else
            {
                return phonenumber;
            }
        }
        public static string FormatPhoneNumber(string? phonenumber)
        {
            if (phonenumber.StartsWith("0") && phonenumber.Length == 10)
            {
                return "256" + phonenumber.Remove(0, 1);
            }
            else if (phonenumber.Length == 9)
            {
                return "256" + phonenumber;
            }
            else
            {
                return phonenumber;
            }
        }

        public static bool IsValidEmailAddress(string? email)
        {
            if (String.IsNullOrEmpty(email))
            {
                return true;
            }
            // Regular expression pattern for validating email addresses
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

            // Create a Regex object with the pattern
            Regex regex = new Regex(pattern);

            // Use the IsMatch method to check if the email matches the pattern
            return regex.IsMatch(email);
        }

        public static Tuple<string, string> EncryptPassword(string password)
        {
            // Generate a random salt
            byte[] salt = GenerateSalt();

            // Hash the password with the salt using PBKDF2 algorithm
            byte[] hashedPassword = HashPassword(password, salt);

            // Combine the salt and hashed password
            byte[] saltedPassword = new byte[salt.Length + hashedPassword.Length];
            Array.Copy(salt, saltedPassword, salt.Length);
            Array.Copy(hashedPassword, 0, saltedPassword, salt.Length, hashedPassword.Length);

            // Convert the salted password to Base64 string
            string encryptedPassword = Convert.ToBase64String(saltedPassword);
            return Tuple.Create(encryptedPassword, Convert.ToBase64String(salt));
        }

        public static string GenrateEncryptedPassword(string password,string saltstring)
        {
            // Generate a random salt
            byte[] salt = Convert.FromBase64String(saltstring);

            // Hash the password with the salt using PBKDF2 algorithm
            byte[] hashedPassword = HashPassword(password, salt);

            // Combine the salt and hashed password
            byte[] saltedPassword = new byte[salt.Length + hashedPassword.Length];
            Array.Copy(salt, saltedPassword, salt.Length);
            Array.Copy(hashedPassword, 0, saltedPassword, salt.Length, hashedPassword.Length);

            // Convert the salted password to Base64 string
            string encryptedPassword = Convert.ToBase64String(saltedPassword);
            return encryptedPassword;
        }

        private static byte[] GenerateSalt()
        {
            // Generate a cryptographically secure random salt
            byte[] salt = new byte[16]; // 16 bytes = 128 bits
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        private static byte[] HashPassword(string password, byte[] salt)
        {
            int iterations = 10000; // Number of PBKDF2 iterations

            // Create a new instance of the Rfc2898DeriveBytes class
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                return pbkdf2.GetBytes(32); // 32 bytes = 256 bits (AES key size)
            }
        }

        public static string GenerateAgentAccountNumber(string? agentId)
        {
            var now = DateTime.Now;
            return agentId + now.ToString("MM")+now.ToString("dd")+now.ToString("HH")+now.ToString("mm")+now.ToString("ss"); 
        }

        public static string GetGLAccountNumber(string glcode)
        {
            var rand = new Random().Next(100000,999999);
            return rand.ToString()+glcode;
        }
        public static string GetAgentId(long recordId)
        {
            try
            {
                if (recordId.ToString().Length == 1)
                    return "10000" + recordId.ToString();
                else if (recordId.ToString().Length == 2)
                    return "1000" + recordId.ToString();
                else if (recordId.ToString().Length == 3)
                    return "100" + recordId.ToString();
                else if (recordId.ToString().Length == 4)
                    return "10" + recordId.ToString();
                else if (recordId.ToString().Length == 5)
                    return "1" + recordId.ToString();
                else
                    return recordId.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }        
    }
}

