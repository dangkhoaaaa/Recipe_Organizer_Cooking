using System;
using System.Collections.Generic;

namespace Services.Models
{
    public partial class Day
    {
        public Day()
        {
            Sessions = new HashSet<Session>();
        }

        public int DayId { get; set; }
        public int PlanId { get; set; }
        public string DayOfWeek { get; set; } = null!;

        public virtual MealPlanning Plan { get; set; } = null!;
        public virtual ICollection<Session> Sessions { get; set; }
    }
}
