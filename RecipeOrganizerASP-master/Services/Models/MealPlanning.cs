using Microsoft.AspNetCore.Identity;
using Services.Models.Authentication;
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
        public string UserId { get; set; }
        public string WeekStartDate { get; set; }

        public virtual AppUser User { get; set; } = null!;
        public virtual ICollection<Day> Days { get; set; }
    }
}
