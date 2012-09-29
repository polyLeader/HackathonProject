using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.Domain
{
    public class User
    {
        public int Id { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public int RoleId { get; set; }

        public string Hash { get; set; }

        public string Street { get; set; }

        public string House { get; set; }

        public int? Flat { get; set; }

        public string Party { get; set; }

        public string PhoneNumber { get; set; }

    }
}