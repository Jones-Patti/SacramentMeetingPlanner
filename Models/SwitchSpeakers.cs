using System;
using System.ComponentModel.DataAnnotations;

namespace SacramentMeetingPlanner.Models
{
    public class SwitchSpeakers
    {

        [Required]
        public int talk1 { get; set;  }

        [Required]
        public int talk2 { get; set;  }


    }
}
