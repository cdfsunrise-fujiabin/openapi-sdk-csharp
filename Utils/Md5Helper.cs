using System;
using System.Text;
using System.Security.Cryptography;

namespace openapi_sdk.Utils
{
    public class Md5Helper
    {
        public static byte[] ComputeMD5Hash(string input)
        {
            // 创建MD5实例
            using (MD5 md5 = MD5.Create())
            {
                // 将输入字符串转换为字节序列
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);

                // 计算哈希值
                return md5.ComputeHash(inputBytes);
            }
        }
    }
}