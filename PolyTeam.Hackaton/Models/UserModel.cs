using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PolyTeam.Hackaton.Models
{
    public class UserModel
    {

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string Street { get; set; }

        public string House { get; set; }

        public int? Flat { get; set; }

    }
}