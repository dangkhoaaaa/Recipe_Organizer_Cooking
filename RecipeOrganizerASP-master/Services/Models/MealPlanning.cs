using System;
using System.Collections.Generic;

namespace Services.Models
{
    public partial class MealPlanning
    {
        public MealPlanning()
        {
            Days = new HashSet<Day>();
        }

        public int PlanId { get; set; }
        public int UserId { get; set; }
        public DateTime WeekStartDate { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Day> Days { get; set; }
    }
}
