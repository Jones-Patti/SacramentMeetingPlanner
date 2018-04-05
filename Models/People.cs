﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SacramentMeetingPlanner.Models
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
        [Required]
        [StringLength(15)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(15)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public ICollection<Bishopric> Bishopric { get; set; }
        public ICollection<Sacrament> SacramentClosingPrayerNavigation { get; set; }
        public ICollection<Sacrament> SacramentOpeningPrayerNavigation { get; set; }
        public ICollection<Speaker> Speaker { get; set; }
    }
}
