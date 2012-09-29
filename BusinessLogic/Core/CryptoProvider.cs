<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

using BusinessLogic.Domain;

namespace BusinessLogic.Core
{
    public class CryptoProvider : ICryptoProvider
    {

        public string EncryptString(string userPassword)
        {
            var md5 = MD5.Create();
            var encoding = Encoding.UTF8;
            return Convert.ToBase64String(md5.ComputeHash(encoding.GetBytes(userPassword)));
        }

        public string CreateHash(string userPassword)
        {
            userPassword = EncryptString(userPassword);
            return EncryptString(userPassword);
        }

        public bool ComparePassword(string userPassword, string enteredPassword)
        {
            return CreateHash(enteredPassword) == userPassword;
        }

        public string GenerateCode(int length = 7)
        {
            string Chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHI JKLMNOPRQSTUVWXYZ0123456789";
            string CodedString = "";
            int LenCodedString = Chars.Count() - 1;

            Random rnd = new Random();

            while (CodedString.Count() < LenCodedString)
            {
                CodedString += Chars[rnd.Next(0, Chars.Count() - 1)];
            }

            return CodedString;
        }
    }
}
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

using BusinessLogic.Domain;

namespace BusinessLogic.Core
{
    public class CryptoProvider : ICryptoProvider
    {

        public string EncryptString(string userPassword)
        {
            var md5 = MD5.Create();
            var encoding = Encoding.UTF8;
            return Convert.ToBase64String(md5.ComputeHash(encoding.GetBytes(userPassword)));
        }

        public string CreateHash()
        {
            return GenerateCode();
        }

        public string ComparePassword(string userPassword, string enteredPassword)
        {
            if (userPassword == EncryptString(enteredPassword))
            {
                return CreateHash();
            }

            return null;
        }

        public string GenerateCode(int length = 7)
        {
            string Chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHI JKLMNOPRQSTUVWXYZ0123456789";
            string CodedString = "";
            int LenCodedString = Chars.Count() - 1;

            Random rnd = new Random();

            while (CodedString.Count() < LenCodedString)
            {
                CodedString += Chars[rnd.Next(0, Chars.Count() - 1)];
            }

            return CodedString;
        }
    }
}
>>>>>>> 32d43352dba0c8bde90f73c51f712cd619264b4b
