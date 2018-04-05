using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SacramentMeetingPlanner.Models
{
    public partial class Bishopric
    {
        public Bishopric()
        {
            Sacrament = new HashSet<Sacrament>();
        }

        public int BishopricId { get; set; }
        [Required]
        [Display(Name = "Member Name")]
        public int PeopleId { get; set; }
        public bool Active { get; set; }
        [Required]
        [StringLength(30)]
        [Display(Name = "Bishopric Title")]
        public string BishopricTitle { get; set; }

        public People People { get; set; }
        public ICollection<Sacrament> Sacrament { get; set; }

       
    }
}
