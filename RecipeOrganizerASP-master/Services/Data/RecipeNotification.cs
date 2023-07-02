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
        public AppUser user { get; set; }
        public Recipe recipe {get; set;}
        public Notification notification { get; set;}
    }
}
