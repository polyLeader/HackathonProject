using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using BusinessLogic.Domain;

namespace BusinessLogic.Core
{
    public class UserProcessor : IUserProcessor
    {
        private readonly IUserRepository userRepository;

        private readonly ICryptoProvider cryptoProvider;

        public UserProcessor(IUserRepository userRepository, ICryptoProvider cryptoProvider)
        {
            this.userRepository = userRepository;
            this.cryptoProvider = cryptoProvider;
        }


        public bool CreateUser(User user)
        {
            //var CurUser = this.userRepository.GetUserByName(user.Name);
            if (GetUserByName(user.Login) != null)
            {
                return false;
            }

            var newUser = new User() {
                LastName = user.LastName,
                SecondName = user.SecondName,
                Login = user.Login,
                Hash = this.cryptoProvider.EncryptString(user.Password),
                RoleId = 2,
                Street = user.Street,
                House = user.House,
                Flat = user.Flat,
                Party = user.Party,
                PhoneNumber = user.PhoneNumber
            };

            this.userRepository.CreateUser(newUser);
            return true;
        }

        public bool LogOn(string userName, string userPassword)
        {
            //User user = new User(GetUserByName(userName));
            var user = this.userRepository.GetByName(userName);
            if (user == null)
            {
                return false;
            }

            if (this.cryptoProvider.ComparePassword(user.Password, userPassword))
            {
                FormsAuthentication.SetAuthCookie(user.Login, true);
                return true;
            }

            return false;
        }

        public User GetUserByName(string userName)
        {
            try
            {
                return this.userRepository.GetByName(userName);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public bool SetRoleToUser(string userName, string roleName)
        {
            throw new NotImplementedException();
        }
    }
}