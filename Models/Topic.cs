using System;
using System.Collections.Generic;

namespace SacramentMeetingPlanner.Models
{
    public partial class Topic
    {
        public Topic()
        {
            Speaker = new HashSet<Speaker>();
        }

        public int TopicId { get; set; }
        public string TopicTitle { get; set; }

        public ICollection<Speaker> Speaker { get; set; }
    }
}
