using System;
using System.Collections.Generic;

namespace SacramentMeetingPlanner.Models
{
    public partial class Speaker
    {
        public int SpeakerId { get; set; }
        public int PeopleId { get; set; }
        public int SacramentId { get; set; }
        public int SpeakerOrder { get; set; }
        public int TopicId { get; set; }


        public People People { get; set; }
        public Sacrament Sacrament { get; set; }
        public Topic Topic { get; set; }
    }
}
