﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace PolyTeam.Hackaton.Models
{
    public class SocialRequestModel
    {
        public IList<SelectListItem> ProblemList { get; set; }
        public virtual DeputyModel Deputy { get; set; }
        public int ProblemId { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public int ? Flat { get; set; }
    }
}