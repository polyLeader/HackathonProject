using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BusinessLogic.Domain;

namespace BusinessLogic.Core
{
    public interface IUserRepository
    {
        void CreateUser(User user);

        void UpdateUser(User user);

        void DeleteUser(int userId);

        User GetByName(string userName);

        bool IsDeputy(string roleName);

        string GetRole(string userName);

        User GetById(int id);

        int GetCount();
    }
}
