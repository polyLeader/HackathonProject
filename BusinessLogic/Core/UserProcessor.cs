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
        private readonly IUserRepository _userRepository;

        private readonly ICryptoProvider _cryptoProvider;

        public UserProcessor(IUserRepository userRepository, ICryptoProvider cryptoProvider)
        {
            _userRepository = userRepository;
            _cryptoProvider = cryptoProvider;
        }


        public bool CreateUser(User user)
        {
            //var CurUser = this.userRepository.GetUserByName(user.Name);
            if (GetUserIdByName(user.Login) != -1)
            {
                return false;
            }

            var newUser = new User()
            {
                LastName = user.LastName,
                SecondName = user.SecondName,
                Login = user.Login,
                Hash = _cryptoProvider.EncryptString(user.Hash),
                RoleId = 2,
                Street = user.Street,
                House = user.House,
                Flat = user.Flat,
                Party = null,
                PhoneNumber = user.PhoneNumber
            };

            _userRepository.CreateUser(newUser);
            return true;
        }

        public bool LogOn(string userName, string userPassword)
        {
            //User user = new User(GetUserByName(userName));
            var user = _userRepository.GetByName(userName);
            if (user == null)
            {
                return false;
            }

            if (_cryptoProvider.ComparePassword(user.Hash, userPassword))
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
                return _userRepository.GetByName(userName);
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public int GetUserIdByName(string userName)
        {
            try
            {
                return _userRepository.GetIdByName(userName);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public User GetUserById(int userId)
        {
            try
            {
                return _userRepository.GetById(userId);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}