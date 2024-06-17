using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace openapi_sdk.Utils
{
    public class RsaHelper
    {
        private static readonly Encoding Encoder = Encoding.UTF8;

        /// <summary>
        /// 公钥加密
        /// </summary>
        /// <param name="xmlPublicKey">公钥</param>
        /// <param name="encryptString">加密字符串</param>
        /// <returns></returns>
        public static string Encrypt(string publicKeyStr, string encryptString)
        {
            using (RSACryptoServiceProvider rsa = RsaUtil.LoadPublicKey(publicKeyStr))
            {
                Byte[] plaintextData = Encoder.GetBytes(encryptString);
                int MaxBlockSize = rsa.KeySize / 8 - 11;//加密块最大长度限制

                if (plaintextData.Length <= MaxBlockSize)
                {
                    return Base64UrlEncode(Convert.ToBase64String(rsa.Encrypt(plaintextData, RSAEncryptionPadding.Pkcs1)));
                }

                using (MemoryStream PlaiStream = new MemoryStream(plaintextData))
                using (MemoryStream CrypStream = new MemoryStream())
                {
                    Byte[] Buffer = new Byte[MaxBlockSize];
                    int BlockSize = PlaiStream.Read(Buffer, 0, MaxBlockSize);

                    while (BlockSize > 0)
                    {
                        Byte[] toEncrypt = new Byte[BlockSize];
                        Array.Copy(Buffer, 0, toEncrypt, 0, BlockSize);

                        Byte[] Cryptograph = rsa.Encrypt(toEncrypt, RSAEncryptionPadding.Pkcs1);
                        CrypStream.Write(Cryptograph, 0, Cryptograph.Length);

                        BlockSize = PlaiStream.Read(Buffer, 0, MaxBlockSize);
                    }

                    return Base64UrlEncode(Convert.ToBase64String(CrypStream.ToArray()));
                }
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="xmlPublicKey"></param>
        /// <param name="EncryptString"></param>
        /// <returns></returns>
        public static string Decrypt(string privateKeyStr, string EncryptString)
        {
            using (RSACryptoServiceProvider RSACryptography = RsaUtil.LoadPrivateKey(privateKeyStr))
            {
                Byte[] CiphertextData = Convert.FromBase64String(EncryptString);
                int MaxBlockSize = RSACryptography.KeySize / 8;    //解密块最大长度限制

                if (CiphertextData.Length <= MaxBlockSize)
                {
                    return Base64UrlDecode(Encoder.GetString(RSACryptography.Decrypt(CiphertextData, false)));
                }

                using (MemoryStream CrypStream = new MemoryStream(CiphertextData))
                using (MemoryStream PlaiStream = new MemoryStream())
                {
                    Byte[] Buffer = new Byte[MaxBlockSize];
                    int BlockSize = CrypStream.Read(Buffer, 0, MaxBlockSize);

                    while (BlockSize > 0)
                    {
                        Byte[] ToDecrypt = new Byte[BlockSize];
                        Array.Copy(Buffer, 0, ToDecrypt, 0, BlockSize);

                        Byte[] Plaintext = RSACryptography.Decrypt(ToDecrypt, false);
                        PlaiStream.Write(Plaintext, 0, Plaintext.Length);

                        BlockSize = CrypStream.Read(Buffer, 0, MaxBlockSize);
                    }

                    return Base64UrlDecode(Encoder.GetString(PlaiStream.ToArray()));
                }
            }
        }

        public static string Base64UrlEncode(string base64)
        {
            base64 = base64.Split('=')[0]; // 移除尾部的 '='
            base64 = base64.Replace('+', '-'); // 将 '+' 替换为 '-'
            base64 = base64.Replace('/', '_'); // 将 '/' 替换为 '_'
            return base64;
        }

        public static string Base64UrlDecode(string base64)
        {
            base64 = base64.Replace('-', '+'); // 将 '-' 还原为 '+'
            base64 = base64.Replace('_', '/'); // 将 '_' 还原为 '/'

            switch (base64.Length % 4) // 根据Base64编码规则，可能需要在末尾添加 '='
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }

            return Encoder.GetString(Convert.FromBase64String(base64)); // 执行Base64解码
        }
    }
}
