using System;
using System.Data;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

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
            catch (Exception ex)
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
    }
}

