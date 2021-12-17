using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.IO;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;

namespace SkyCommerce.Data.Configuration
{
    public class DataProtectionConverter : ValueConverter<string, string>
    {
        private static readonly byte[] Chave;

        static DataProtectionConverter()
        {
            if (!File.Exists("efkey"))
            {
                byte[] data = new byte[128];
                var rng = RandomNumberGenerator.Create();
                rng.GetBytes(data);
                File.WriteAllText("current.key", Encoding.Default.GetString(data));
                Chave = data;
            }
            else
            {
                Chave = Encoding.UTF8.GetBytes(File.ReadAllText("efkey"));
            }
        }
        public DataProtectionConverter()
            : base(ConvertTo, ConvertFrom, default)
        {

        }

        private static readonly Expression<Func<string, string>> ConvertTo = x => LockView(x);
        private static readonly Expression<Func<string, string>> ConvertFrom = x => UnLockView(x);

        static string LockView(string texto)
        {
            using var hashProvider = new MD5CryptoServiceProvider();
            var encriptar = new TripleDESCryptoServiceProvider
            {
                Mode = CipherMode.ECB,
                Key = hashProvider.ComputeHash(Chave),
                Padding = PaddingMode.PKCS7
            };

            using var transforme = encriptar.CreateEncryptor();
            var dados = Encoding.UTF8.GetBytes(texto);
            return Convert.ToBase64String(transforme.TransformFinalBlock(dados, 0, dados.Length));
        }

        static string UnLockView(string texto)
        {
            using var hashProvider = new MD5CryptoServiceProvider();
            var descriptografar = new TripleDESCryptoServiceProvider
            {
                Mode = CipherMode.ECB,
                Key = hashProvider.ComputeHash(Chave),
                Padding = PaddingMode.PKCS7
            };

            using var transforme = descriptografar.CreateDecryptor();
            var dados = Convert.FromBase64String(texto.Replace(" ", "+"));
            return Encoding.UTF8.GetString(transforme.TransformFinalBlock(dados, 0, dados.Length));
        }
    }
}