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
        public int UserId { get; set; }
        public int? DeputyId { get; set; }
        public int ProblemId { get; set; }
        public int StreetId { get; set; }
        public string House { get; set; }
        public int? Flat { get; set; }
        public bool? Done { get; set; }
        public DateTime CreatingDate { get; set; }
        public DateTime? FinishDate { get; set; }
    }
}
