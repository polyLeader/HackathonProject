using System;
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
        private readonly DatabaseContext _databaseContext;
        //private readonly IUserRepository userRepository;

        public UserRepository(DatabaseContext dataBaseContext)
        {
            this._databaseContext = dataBaseContext;
            // this.userRepository = userRepository;
        }

        public void CreateUser(User user)
        {
            this._databaseContext.Entry(user).State = EntityState.Added;
            this._databaseContext.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            this._databaseContext.Entry(user).State = EntityState.Modified;
            this._databaseContext.SaveChanges();
        }

        public void DeleteUser(int userId)
        {
            User user = this._databaseContext.Users.Single(x => x.Id == userId);
            this._databaseContext.Users.Remove(user);
            this._databaseContext.SaveChanges();


        }

        public User GetByName(string userName)
        {
            try
            {
                return this._databaseContext.Users.ToList().Single(it => it.Login == userName);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public string GetRole(string userName)
        {
            return this._databaseContext.Roles.ToList().Where(it => it.Id == this.GetByName(userName).RoleId).Select(x => x.Name).First();
        }

        public bool IsDeputy(string userName)
        {
            return System.Web.Security.Roles.IsUserInRole(userName, "Deputy");
        }

        public User GetById(int id)
        {
            return _databaseContext.Users.FirstOrDefault(x => x.Id == id);
        }

        public int GetCount()
        {
            return _databaseContext.Users.Count();
        }
    }

     
}