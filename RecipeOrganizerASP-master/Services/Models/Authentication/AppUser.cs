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
        public bool Status { get; set; }
    }
}
