using System;
using System.Text;
using System.Security.Cryptography;

namespace openapi_sdk.Utils
{
    public class Md5Helper
    {
        public static string ComputeMD5Hash(string input)
        {
            // 创建MD5实例
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}