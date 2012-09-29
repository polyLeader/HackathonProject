using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PolyTeam.Hackaton.Models
{
    public class SocialRequestModel
    {
        public int Id { get; set; }
       // public User User { get; set; }
        public ProblemModel ProblemModel { get; set; }
        public int ProblemId { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public int ? Flat { get; set; }
    }
}