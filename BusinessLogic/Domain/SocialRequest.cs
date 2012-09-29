using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogic.domen;

namespace BusinessLogic.Domain
{
    public class SocialRequest
    {
        public int Id { get; set; }
        public Requster Requster { get; set; }
        public Problem Problem { get; set; }
        public Deputy Deputy { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string Flat { get; set; }
    }
}
