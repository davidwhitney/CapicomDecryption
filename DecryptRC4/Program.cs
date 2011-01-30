using System;
using System.Security.Cryptography.Xml;
using System.Text;
using Mono.Security.Cryptography;

namespace Capicom
{
    class Program
    {
        private const string EncryptionMarker = "3A18808DF32C2aa99044AAD2564CB570881A32D23CD93805930C719827BFB2C7";
        private const string EncryptionSecret = "63A1DA535D9C05beB9DD81061BD939FB41AB99A7CD7B19cb8DAD69F3AEBF7625";

        static void Main(string[] args)
        {

            var encryptedStringInBase64 = @"MIIFPAYJKwYBBAGCN1gDoIIFLTCCBSkGCisGAQQBgjdYAwGgggUZMIIFFQIDAgAA
AgJoAQICAIAEAAQQew/csFujZyzS1j8kGXUPnwSCBPBaQWwlDwYRiKexGe8gc393
yhUtkKYt5ZVohJsk5NCXvUGm16/2NcF7m2tZ6YgaKaSlSFgwra3cRT6JItz6s+Oi
wCOkxIjYf10T3gyKUfeSrqlgjQulSZ8Ia2LmzomP8Gx9HffCeHEs8zn8lcxv8s4P
Sa3qrPaOUE1H5W5xhRHuhIIRs4I8B3DF3dTxnweMlSUH4IWYLc7w8vyVVLm4CjZE
Ak5Gir23IQxOYIUEv/ZmBJI/nLKCfCnGlESyKD2EMHOpO7mBlrv74BvS9MpOQamX
u63M1X3aPF30roIVZUFvsKIOLvSaDzVyYcpUCC8oJnZmtW+VvCyXsFUzmmBNu7aB
uKEPfXvBvlL4mZj1UtHh0AKfuv8Ju27k8oGjTO+sku3ox3EBUvNF5THeA6AjojSt
fdcS2b0U5reykhaI8nZK4KVCPRIzN9GdWHFk/GC7UQ0N8ss/3+A8itrhRbJ8SPdj
5tKRfHzdtFNqClUmNCkR+t/D4eNu0kxEB9LxEcYfvwkqUhk02Sf1tSUOPOdVprXQ
ChPmNKm0Q9El8XSe4bkFVBsIvkMOOHExp7XLFWVT8Opx6JlfJFuCoxn1kTLvZGYl
6yk7aIznANZj2jfeOpTClXNJeSqEVBZV+fAyLKTJxzM4ywE6qPFaAcOq6P7XnQ1E
y04eJXx6ZPtXfWE2yNZ5x42XOs7+mwdhRnlD+nBr/3q/jzGhuTOmc/iFoCfJA4wf
ApTz0XexKITLPk81axXGoEU2FBcNHupdy0RzPI/Bqy9jC3tHGLphgBFm99lIkU2w
lUzguszHaIxmk7ynnLOOXIr1LWKatx4JtX9hNPvvG+A33xcLRXYbpnCoP9CQzDTo
EUWvJmNAiyRZyxt8auSv4sp+Ss/+THhHO8+FaoTqq/pF2KAbsJRdPu2Mk+i0x47e
+gMpqVBpcWkhV05Ok6iYLdmjSq/bM3ejhhb0RanK34bpLsvpQx8D4eUWZi9Kizw+
1NWsOXzM+7iptv2UJvU8lOa+upcNHx5HLSUeL2oamqPGjmjAv3b1BvqgGJBhXqns
Ka44HNCzXGMUMH8tYk+DBSdKXV2sDwT+7hyfOYs8OJ8gv6CGKnpIxTHAa+QZ++F1
PCP87rZMy/uhCDvMXf6/EDY+Ibn/0J1lJBO8qvKTlJnYw0W4W5uLO9nxplserN2+
cqWbccYHW2GAY2MhcB69ssBJjvZwnwe7H+4JgHxyWbceGd5XZD2fmwlhz3++t+Vh
EJXXIPqBtHas5NP/PMeb020SvOqUAwIUCR5w8Urd7hE7ItsOy8WpowR3Vlk0x3ow
+pzI1RusM36WltrU0LL76UHrL5ntn0KTM3vwJIcbOf92F66fbTf5sZvPrGwpNVfA
Iu34Od3zvP1A032LN9JdIEAqtI1ZCNu8XTSzhnJwa2PZ7TxJ5o3N9QUlFiYkgiqo
IummL/4s+C9nkoOdtNdlDPEsOmYbu79bpobj0EZKBbJKns3gZctZ6e7Ylc4vBj3g
b6ONMOiOvgE91B9n2+alOfQsN4G9U+rPRIHvEdbzhhMJJfj0ne5Onn6GRYrxfAVK
SC/s/uUwphwazVZNdxTwSFr1eD7pQiJeyQWuVyFu+jFKdW640raXVDw7aLqOgOF5
Aio2CYb+Jh6hCEXlxl/FNvZw0TOeVgOTSUL/C6HX8OHrj3utC/HyQ45i8eN25kcq";

            var rawEncodedBytes = Convert.FromBase64String(encryptedStringInBase64);
            
            var arcfour = RC4.Create();
            arcfour.KeySize = 128;


            //var key = Encoding.Default.GetBytes(EncryptionSecret);
            //var data = rawEncodedBytes;

            var key = Encoding.Default.GetBytes(EncryptionSecret);
            var data = Encoding.UTF8.GetBytes(encryptedStringInBase64);


            var output = new byte[data.Length];

            var decoder = arcfour.CreateDecryptor(key, new byte[0]);
            decoder.TransformBlock(data, 0, data.Length, output, 0);

            var plain = Encoding.Default.GetString(output);
            var result = ByteArrayToHexString(output);

        }

        public static string Decrypt(string encrypted, string password)
        {
            var data = new QtvEncryptedData();
            var algorithm = data.Algorithm;
            algorithm.Name = EncryptionAlgorithm.RC4;
            algorithm.KeyLength = EncryptionKeyLength.Bits128;
            data.SetSecret(password, SecretType.Password);
            data.Decrypt(encrypted);
            return data.Content;
        }
 

        public static string Encrypt(string plain, string password)
        {
            var data = new QtvEncryptedData();
            data.SetSecret(password, SecretType.Password);
            data.Content = plain;
            var algorithm = data.Algorithm;
            algorithm.Name = EncryptionAlgorithm.RC4;
            algorithm.KeyLength = EncryptionKeyLength.Bits128;
            return data.Encrypt(EncodingType.Base64);
        }

        public static string ByteArrayToHexString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
    }
}