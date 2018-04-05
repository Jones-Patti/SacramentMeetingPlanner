using System;
using System.Collections.Generic;

namespace SacramentMeetingPlanner.Data
{
    public partial class Hymn
    {
        public Hymn()
        {
            SacramentClosingHymnNavigation = new HashSet<Sacrament>();
            SacramentIntermediateHymnNavigation = new HashSet<Sacrament>();
            SacramentOpeningHymnNavigation = new HashSet<Sacrament>();
            SacramentSacramentHymnNavigation = new HashSet<Sacrament>();
        }

        public int HymnId { get; set; }
        public string HymnTitle { get; set; }

        public ICollection<Sacrament> SacramentClosingHymnNavigation { get; set; }
        public ICollection<Sacrament> SacramentIntermediateHymnNavigation { get; set; }
        public ICollection<Sacrament> SacramentOpeningHymnNavigation { get; set; }
        public ICollection<Sacrament> SacramentSacramentHymnNavigation { get; set; }
    }
}
