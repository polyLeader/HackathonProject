using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogic.Domain;

namespace BusinessLogic.Domain
{
    public class SocialRequest
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Problem Problem { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public int? Flat { get; set; }
    }
}
