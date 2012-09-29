using System;
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

        public string WordTranslator(string russianWord)
        {
            russianWord.Replace("а", "a");
            russianWord.Replace("б", "b");
            russianWord.Replace("в", "v");
            russianWord.Replace("г", "g");
            russianWord.Replace("д", "d");
            russianWord.Replace("е", "e");
            russianWord.Replace("ё", "yo");
            russianWord.Replace("ж", "zh");
            russianWord.Replace("з", "z");
            russianWord.Replace("и", "i");
            russianWord.Replace("й", "j");
            russianWord.Replace("к", "k");
            russianWord.Replace("л", "l");
            russianWord.Replace("м", "m");
            russianWord.Replace("н", "n");
            russianWord.Replace("о", "o");
            russianWord.Replace("п", "p");
            russianWord.Replace("р", "r");
            russianWord.Replace("с", "s");
            russianWord.Replace("т", "t");
            russianWord.Replace("у", "u");
            russianWord.Replace("ф", "f");
            russianWord.Replace("х", "h");
            russianWord.Replace("ц", "c");
            russianWord.Replace("ч", "ch");
            russianWord.Replace("ш", "sh");
            russianWord.Replace("щ", "sch");
            russianWord.Replace("ъ", "j");
            russianWord.Replace("ы", "i");
            russianWord.Replace("ь", "j");
            russianWord.Replace("э", "e");
            russianWord.Replace("ю", "yu");
            russianWord.Replace("я", "ya");
            russianWord.Replace("А", "A");
            russianWord.Replace("Б", "B");
            russianWord.Replace("В", "V");
            russianWord.Replace("Г", "G");
            russianWord.Replace("Д", "D");
            russianWord.Replace("Е", "E");
            russianWord.Replace("Ё", "Yo");
            russianWord.Replace("Ж", "Zh");
            russianWord.Replace("З", "Z");
            russianWord.Replace("И", "I");
            russianWord.Replace("Й", "J");
            russianWord.Replace("К", "K");
            russianWord.Replace("Л", "L");
            russianWord.Replace("М", "M");
            russianWord.Replace("Н", "N");
            russianWord.Replace("О", "O");
            russianWord.Replace("П", "P");
            russianWord.Replace("Р", "R");
            russianWord.Replace("С", "S");
            russianWord.Replace("Т", "T");
            russianWord.Replace("У", "U");
            russianWord.Replace("Ф", "F");
            russianWord.Replace("Х", "H");
            russianWord.Replace("Ц", "C");
            russianWord.Replace("Ч", "Ch");
            russianWord.Replace("Ш", "Sh");
            russianWord.Replace("Щ", "Sch");
            russianWord.Replace("Ъ", "J");
            russianWord.Replace("Ы", "I");
            russianWord.Replace("Ь", "J");
            russianWord.Replace("Э", "E");
            russianWord.Replace("Ю", "Yu");
            russianWord.Replace("Я", "Ya");

            return russianWord;
        }

        public string GenerateDeputyLogin(string firstName, string lastName)
        {
            var login = WordTranslator(firstName) + "_" + WordTranslator(lastName);
            return login;
        }
    }
}
