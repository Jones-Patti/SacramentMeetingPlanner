using System;
using System.Collections.Generic;

namespace SacramentMeetingPlanner.Data
{
    public partial class Bishopric
    {
        public Bishopric()
        {
            Sacrament = new HashSet<Sacrament>();
        }

        public int BishopricId { get; set; }
        public int PeopleId { get; set; }
        public sbyte Active { get; set; }

        public People People { get; set; }
        public ICollection<Sacrament> Sacrament { get; set; }
    }
}
