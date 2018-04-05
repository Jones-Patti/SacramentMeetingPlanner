using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SacramentMeetingPlanner.Models
{
    public class SacramentViewModel
    {
        public IEnumerable<Sacrament> Sacraments { get; set; }
        public IEnumerable<Speaker> Speakers { get; set; }

    }
}