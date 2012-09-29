﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web.Security;
using BusinessLogic.Domain;

using BusinessLogic.Domain;

namespace BusinessLogic.Core
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext dataBaseContext;
        //private readonly IUserRepository userRepository;

        public UserRepository(DatabaseContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
           // this.userRepository = userRepository;
        }

        public void CreateUser(User user)
        {
            this.dataBaseContext.Entry(user).State = EntityState.Added;
            this.dataBaseContext.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            this.dataBaseContext.Entry(user).State = EntityState.Modified;
            this.dataBaseContext.SaveChanges();
        }

        public void DeleteUser(int userId)
        {
            User user = this.dataBaseContext.Users.Single(x => x.Id == userId);
            this.dataBaseContext.Users.Remove(user);
            this.dataBaseContext.SaveChanges();


        }

        public User GetByName(string userName)
        {
            try
            {
                return this.dataBaseContext.Users.ToList().Single(it => it.Login == userName);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public bool IsDeputy(string userName)
        {
            return System.Web.Security.Roles.IsUserInRole(userName, "Deputy");
        }
    }
}
