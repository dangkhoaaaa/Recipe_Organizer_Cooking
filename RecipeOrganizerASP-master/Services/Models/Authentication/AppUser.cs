using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Authentication
{
    public class AppUser : IdentityUser
    {
        public virtual ICollection<Collection> Collections { get; set; }
        public virtual ICollection<MealPlanning> MealPlannings { get; set; }
        public virtual ICollection<Metadata> MetaData { get; set; }
    }
}
