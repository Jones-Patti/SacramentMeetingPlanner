using System;
using System.Collections.Generic;

namespace SacramentMeetingPlanner.Data
{
    public partial class People
    {
        public People()
        {
            Bishopric = new HashSet<Bishopric>();
            SacramentClosingPrayerNavigation = new HashSet<Sacrament>();
            SacramentOpeningPrayerNavigation = new HashSet<Sacrament>();
            Speaker = new HashSet<Speaker>();
        }

        public int PeopleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Bishopric> Bishopric { get; set; }
        public ICollection<Sacrament> SacramentClosingPrayerNavigation { get; set; }
        public ICollection<Sacrament> SacramentOpeningPrayerNavigation { get; set; }
        public ICollection<Speaker> Speaker { get; set; }
    }
}
