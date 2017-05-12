using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TokenINFRA.Regras
{
    public class Criptografia
    {
        private static SymCryptography _objSenha;

        /// <summary>Gera uma senha criptografada com letras e número aleatórios</summary>
        /// <returns></returns>
        public static string GeraSenhaCriptografada()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            _objSenha = new SymCryptography("Rijndael");
            return _objSenha.Encrypt(builder.ToString());
        }

        private static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        /// <summary>Gera uma string aleatória com o tamanho selecionado</summary>
        /// <param name="size">Tamanho da string</param>
        /// <param name="lowerCase">Se true, gera a string em minúsculo</param>
        /// <returns>String aleatória</returns>
        private static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < size; i++)
            {
                char ch = Convert.ToChar(Convert.ToInt32(26 * random.NextDouble() + 65));
                builder.Append(ch);
            }
            if (lowerCase)
            {
                return builder.ToString().ToLower();
            }
            return builder.ToString();
        }

        /// <summary>Retorna a senha descriptofrada "Rijndael"</summary>
        /// <param name="senha"></param>
        /// <returns></returns>
        public static string RetornaSenhaDescriptografada(string senha)
        {
            _objSenha = new SymCryptography("Rijndael");
            return _objSenha.Decrypt(senha);
        }

        /// <summary>Criptografa a string e retorna.</summary>
        /// <param name="senha"></param>
        /// <returns></returns>
        public static string RetornaSenhaCriptografada(string senha)
        {
            _objSenha = new SymCryptography("Rijndael");
            return _objSenha.Encrypt(senha);
        }
    }

    #region Symmetric cryptography class...

    /// <summary>Contains methods and properties for two-way encryption and decryption</summary>
    /// <author>Chidi C. Ezeukwu</author>
    /// <written>May 24, 2004</written>
    /// <Updated>Aug 07, 2004</Updated>
    internal class SymCryptography
    {
        #region Private members...

        private string _mKey = "TOKEN";
        private string _mSalt = string.Empty;
        private readonly ServiceProviderEnum _mAlgorithm;
        private readonly SymmetricAlgorithm _mCryptoService;

        private void SetLegalIv()
        {
            // Set symmetric algorithm
            switch (_mAlgorithm)
            {
                case ServiceProviderEnum.Rijndael:
                    _mCryptoService.IV = new byte[] { 0xf, 0x6f, 0x13, 0x2e, 0x35, 0xc2, 0xcd, 0xf9, 0x5, 0x46, 0x9c, 0xea, 0xa8, 0x4b, 0x73, 0xcc };
                    break;
                default:
                    _mCryptoService.IV = new byte[] { 0xf, 0x6f, 0x13, 0x2e, 0x35, 0xc2, 0xcd, 0xf9 };
                    break;
            }
        }

        #endregion

        #region Public interfaces...

        public enum ServiceProviderEnum
        {
            // Supported service providers
            Rijndael, //CRIADO EM 1998
            Rc2,
            Des, //JÁ FOI QUEBRADO
            TripleDes
        }

        public SymCryptography()
        {
            // Default symmetric algorithm
            _mCryptoService = new RijndaelManaged { Mode = CipherMode.CBC };
            _mAlgorithm = ServiceProviderEnum.Rijndael;
        }

        public SymCryptography(ServiceProviderEnum serviceProvider)
        {
            // Select symmetric algorithm
            switch (serviceProvider)
            {
                case ServiceProviderEnum.Rijndael:
                    _mCryptoService = new RijndaelManaged();
                    _mAlgorithm = ServiceProviderEnum.Rijndael;
                    break;
                case ServiceProviderEnum.Rc2:
                    _mCryptoService = new RC2CryptoServiceProvider();
                    _mAlgorithm = ServiceProviderEnum.Rc2;
                    break;
                case ServiceProviderEnum.Des:
                    _mCryptoService = new DESCryptoServiceProvider();
                    _mAlgorithm = ServiceProviderEnum.Des;
                    break;
                case ServiceProviderEnum.TripleDes:
                    _mCryptoService = new TripleDESCryptoServiceProvider();
                    _mAlgorithm = ServiceProviderEnum.TripleDes;
                    break;
            }
            _mCryptoService.Mode = CipherMode.CBC;
        }

        public SymCryptography(string serviceProviderName)
        {
            // Select symmetric algorithm
            switch (serviceProviderName.ToLower())
            {
                case "rijndael":
                    serviceProviderName = "Rijndael";
                    _mAlgorithm = ServiceProviderEnum.Rijndael;
                    break;
                case "rc2":
                    serviceProviderName = "RC2";
                    _mAlgorithm = ServiceProviderEnum.Rc2;
                    break;
                case "des":
                    serviceProviderName = "DES";
                    _mAlgorithm = ServiceProviderEnum.Des;
                    break;
                case "tripledes":
                    serviceProviderName = "TripleDES";
                    _mAlgorithm = ServiceProviderEnum.TripleDes;
                    break;
            }

            // Set symmetric algorithm
            _mCryptoService = (SymmetricAlgorithm)CryptoConfig.CreateFromName(serviceProviderName);
            _mCryptoService.Mode = CipherMode.CBC;
        }

        public virtual byte[] GetLegalKey()
        {
            // Adjust key if necessary, and return a valid key
            if (_mCryptoService.LegalKeySizes.Length > 0)
            {
                // Key sizes in bits
                int keySize = _mKey.Length * 8;
                int minSize = _mCryptoService.LegalKeySizes[0].MinSize;
                int maxSize = _mCryptoService.LegalKeySizes[0].MaxSize;
                int skipSize = _mCryptoService.LegalKeySizes[0].SkipSize;

                if (keySize > maxSize)
                {
                    // Extract maximum size allowed
                    _mKey = _mKey.Substring(0, maxSize / 8);
                }
                else if (keySize < maxSize)
                {
                    // Set valid size
                    int validSize = (keySize <= minSize) ? minSize : (keySize - keySize % skipSize) + skipSize;
                    if (keySize < validSize)
                    {
                        // Pad the key with asterisk to make up the size
                        _mKey = _mKey.PadRight(validSize / 8, '*');
                    }
                }
            }
            PasswordDeriveBytes key = new PasswordDeriveBytes(_mKey, Encoding.ASCII.GetBytes(_mSalt));
#pragma warning disable 612, 618
            return key.GetBytes(_mKey.Length);
#pragma warning restore 612, 618
        }

        public virtual string Encrypt(string plainText)
        {
            byte[] plainByte = Encoding.ASCII.GetBytes(plainText);
            byte[] keyByte = GetLegalKey();

            // Set private key
            _mCryptoService.Key = keyByte;
            SetLegalIv();

            // Encryptor object
            ICryptoTransform cryptoTransform = _mCryptoService.CreateEncryptor();

            // Memory stream object
            MemoryStream ms = new MemoryStream();

            // Crpto stream object
            CryptoStream cs = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Write);

            // Write encrypted byte to memory stream
            cs.Write(plainByte, 0, plainByte.Length);
            cs.FlushFinalBlock();

            // Get the encrypted byte length
            byte[] cryptoByte = ms.ToArray();

            // Convert into base 64 to enable result to be used in Xml
            return Convert.ToBase64String(cryptoByte, 0, cryptoByte.GetLength(0));
        }

        public virtual string Decrypt(string cryptoText)
        {
            // Convert from base 64 string to bytes
            byte[] cryptoByte = Convert.FromBase64String(cryptoText);
            byte[] keyByte = GetLegalKey();

            // Set private key
            _mCryptoService.Key = keyByte;
            SetLegalIv();

            // Decryptor object
            ICryptoTransform cryptoTransform = _mCryptoService.CreateDecryptor();
            try
            {
                // Memory stream object
                MemoryStream ms = new MemoryStream(cryptoByte, 0, cryptoByte.Length);

                // Crpto stream object
                CryptoStream cs = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Read);

                // Get the result from the Crypto stream
                StreamReader sr = new StreamReader(cs);
                return sr.ReadToEnd();
            }
            catch
            {
                return null;
            }
        }

        public string Key
        {
            //get
            //{
            //   return mKey;
            //}
            set
            {
                _mKey = value;
            }
        }

        public string Salt
        {
            // Salt value
            get
            {
                return _mSalt;
            }
            set
            {
                _mSalt = value;
            }
        }
        #endregion
    }

    #endregion

    #region Hash Class...

    /// <summary>CHashProtector is a password protection one way encryption algorithm</summary>
    /// <programmer>Chidi C. Ezeukwu</programmer>
    /// <written>May 16, 2004</written>
    /// <Updated>Aug 07, 2004</Updated>
    internal class Hash
    {
        #region Private member variables...

        private string _mSalt;
        private readonly HashAlgorithm _mCryptoService;

        #endregion

        #region Public interfaces...


        /*
         O .NET Framework suporte três algoritmos de hash, a saber: MD5, SHA1e HMAC
        */


        public enum ServiceProviderEnum
        {
            // Supported algorithms
            Sha1,
            Sha256,
            Sha384,
            Sha512,
            Md5
        }

        public Hash()
        {
            // Default Hash algorithm
            _mCryptoService = new SHA1Managed();
        }

        public Hash(ServiceProviderEnum serviceProvider)
        {
            // Select hash algorithm
            switch (serviceProvider)
            {
                case ServiceProviderEnum.Md5:
                    _mCryptoService = new MD5CryptoServiceProvider();
                    break;
                case ServiceProviderEnum.Sha1:
                    _mCryptoService = new SHA1Managed();
                    break;
                case ServiceProviderEnum.Sha256:
                    _mCryptoService = new SHA256Managed();
                    break;
                case ServiceProviderEnum.Sha384:
                    _mCryptoService = new SHA384Managed();
                    break;
                case ServiceProviderEnum.Sha512:
                    _mCryptoService = new SHA512Managed();
                    break;
            }
        }

        public Hash(string serviceProviderName)
        {
            // Set Hash algorithm
            _mCryptoService = (HashAlgorithm)CryptoConfig.CreateFromName(serviceProviderName.ToUpper());
        }

        public virtual string Encrypt(string plainText)
        {
            byte[] cryptoByte = _mCryptoService.ComputeHash(Encoding.ASCII.GetBytes(plainText + _mSalt));

            // Convert into base 64 to enable result to be used in Xml
            return Convert.ToBase64String(cryptoByte, 0, cryptoByte.Length);
        }

        public string Salt
        {
            // Salt value
            get
            {
                return _mSalt;
            }
            set
            {
                _mSalt = value;
            }
        }
        #endregion
    }
    #endregion
}