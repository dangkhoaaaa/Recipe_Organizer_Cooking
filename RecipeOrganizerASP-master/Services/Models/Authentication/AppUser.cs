using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Authentication
{
    public class AppUser : IdentityUser
    {
        public AppUser()
        {
            MealPlannings = new HashSet<MealPlanning>();
            MetaData = new HashSet<Metadata>();
        }
        [Column(TypeName = "nvarchar")]
        [StringLength(100)]
        public string? FirstName { get; set; }
        [Column(TypeName = "nvarchar")]
        [StringLength(100)]
        public string? LastName { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }

        [DefaultValue("true")]
        public bool Status { get; set; }

        [Column(TypeName = "text")]
        public string? Image { get; set; }
        public virtual ICollection<MealPlanning> MealPlannings { get; set; }
        public virtual ICollection<Metadata> MetaData { get; set; }
    }
}
