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

        void GetUserByName(string userName);

        void SetRoleToUser(User user);

    }
}
