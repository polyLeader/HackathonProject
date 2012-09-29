using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BusinessLogic.Domain;

namespace BusinessLogic.Core
{
    public interface IUserProcessor
    {
        bool CreateUser(User user);

        bool LogOn(string userName, string userPassword);

        User GetUserByName(string userName);

        bool CreateRole(string roleName);

        bool SetRoleToUser(string userName, string roleName);
    }
}
