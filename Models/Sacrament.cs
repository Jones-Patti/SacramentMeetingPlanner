using System;
using System.Collections.Generic;

namespace SacramentMeetingPlanner.Models
{
    public partial class Sacrament
    {
        public Sacrament()
        {
            Speaker = new HashSet<Speaker>();
        }

        public int SacramentId { get; set; }
        public DateTime SacramentDate { get; set; }
        public int ConductingBishopric { get; set; }
        public int OpeningPrayer { get; set; }
        public int OpeningHymn { get; set; }
        public int SacramentHymn { get; set; }
        public int? IntermediateHymn { get; set; }
        public int ClosingHymn { get; set; }
        public int ClosingPrayer { get; set; }

        public Hymn ClosingHymnNavigation { get; set; }
        public People ClosingPrayerNavigation { get; set; }
        public Bishopric ConductingBishopricNavigation { get; set; }
        public Hymn IntermediateHymnNavigation { get; set; }
        public Hymn OpeningHymnNavigation { get; set; }
        public People OpeningPrayerNavigation { get; set; }
        public Hymn SacramentHymnNavigation { get; set; }
        public ICollection<Speaker> Speaker { get; set; }
    }
}
