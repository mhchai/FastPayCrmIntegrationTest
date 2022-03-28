using System;
using System.Security.Cryptography;
using System.Text;

namespace FastPayCrmIntegrationTest
{
    public class SignatureHelper
    {
        public static string ComputeSHA256withRSA(string datastring,string privatekey)
        {
            string signatureData;

            byte[] hashedByteArray;

            using (SHA256 sha256hash = SHA256.Create())
            {
                //byte[] bytes = sha256hash.ComputeHash(Encoding.UTF8.GetBytes(datastring));
                hashedByteArray = sha256hash.ComputeHash(Encoding.UTF8.GetBytes(datastring));

                //StringBuilder builtstring = new();

                //for (int i = 0; i < bytes.Length; i++)
                //{
                //    builtstring.Append(bytes[i].ToString("X2"));
                //}

                //hashedData = builtstring.ToString().ToLower();
            }

            using (RSA rsa = RSA.Create())
            {
                rsa.ImportFromPem(privatekey.ToCharArray());

                byte[] encpstr = rsa.Encrypt(hashedByteArray, RSAEncryptionPadding.OaepSHA256);

                //StringBuilder builtstring = new();

                //for (int i = 0; i < encpstr.Length; i++)
                //{
                //    builtstring.Append(encpstr[i].ToString("X2"));
                //}

                signatureData = BitConverter.ToString(encpstr).Replace("-", string.Empty);
            }

            return signatureData;
        }

        public static string ComputeSHA256withRSA3(string datastring, string privatekey)
        {
            byte[] signedBytes;

            using(var rsa = new RSACryptoServiceProvider())
            {
                var encoder = new UTF8Encoding();

                byte[] originalData = encoder.GetBytes(datastring);

                try
                {
                    rsa.ImportFromPem(privatekey.ToCharArray());

                    signedBytes = rsa.SignData(originalData, CryptoConfig.MapNameToOID("SHA256"));

                    return BitConverter.ToString(signedBytes).Replace("-", string.Empty).ToLower();
                }
                catch (CryptographicException e)
                {
                    return e.Message;
                }
            }
        }

        public static string ComputeSHA256withRSA2(string datastring, string privatekey)
        {
            RSA rsa = RSA.Create();
            rsa.ImportFromPem(privatekey.ToCharArray());

            byte[] signature = rsa.Encrypt(Encoding.UTF8.GetBytes(datastring), RSAEncryptionPadding.OaepSHA256);

            return BitConverter.ToString(signature).Replace("-", string.Empty);
        }


    }
}
