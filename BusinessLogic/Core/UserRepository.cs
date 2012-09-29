using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using BusinessLogic.Domain;

namespace BusinessLogic.Core
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext dataBaseContext;

        public UserRepository(DatabaseContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
        }

        public void CreateUser(User user)
        {
            //this.dataBaseContext.Entry
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }

        public void GetUserByName(string userName)
        {
            throw new NotImplementedException();
        }

        public void SetRoleToUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
