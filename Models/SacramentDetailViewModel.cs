using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SacramentMeetingPlanner.Models
{
    public class SacramentDetailViewModel
    {
        
        public Sacrament Sacrament { get; set; }
        public IEnumerable<Speaker> Speakers { get; set; }
        public Speaker Speaker { get; set; }
        public Bishopric Bishopric { get; set;  }
    }
}
