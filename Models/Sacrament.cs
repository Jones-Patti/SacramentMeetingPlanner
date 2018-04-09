using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SacramentMeetingPlanner.Models
{
    public partial class Sacrament
    {
        public Sacrament()
        {
            Speaker = new HashSet<Speaker>();
        }

        public int SacramentId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime SacramentDate { get; set; }

        [Required]
        [Display(Name = "Conduncting")]
        public int ConductingBishopric { get; set; }

        [Required]
        [Display(Name = "Opening Prayer")]
        public int OpeningPrayer { get; set; }

        [Required]
        [Display(Name = "Opening Hymn")]
        public int OpeningHymn { get; set; }

        [Required]
        [Display(Name = "Sacrament Hymn")]
        public int SacramentHymn { get; set; }


        [Display(Name = "Intermediate Hymn")]
        public int? IntermediateHymn { get; set; }

        [Required]
        [Display(Name = "Intermediate Hymn")]
        public int ClosingHymn { get; set; }

        [Required]
        [Display(Name = "Closing Prayer")]
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
