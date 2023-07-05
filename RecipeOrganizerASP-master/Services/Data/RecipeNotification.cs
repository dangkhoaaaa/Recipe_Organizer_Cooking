using Services.Models;
using Services.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Data
{
    public class RecipeNotification
    {
        public AppUser User { get; set; }
        public Recipe Recipe {get; set;}
        public Notification Notification { get; set;}
    }
}
