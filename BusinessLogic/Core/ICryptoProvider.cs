using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.Core
{
    public interface ICryptoProvider
    {
        string EncryptString(string userPassword);

        string CreateHash();

        string ComparePassword(string userPassword, string enteredPassword);

        string GenerateCode(int length = 7);

        string GenerateDeputyLogin(string firstName, string lastName);
    }
}
