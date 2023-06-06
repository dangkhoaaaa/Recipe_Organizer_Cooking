using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Services.Models
{
    public partial class User
    {
        public User()
        {
            Collections = new HashSet<Collection>();
            MealPlannings = new HashSet<MealPlanning>();
            MetaData = new HashSet<Metadata>();
        }

        public int UserId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "User Required")]
        [StringLength(25, ErrorMessage = "Username cannot exceed {1} characters.")]
        public string Username { get; set; } = null!;
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password Required")]
        [StringLength(25, ErrorMessage = "Password cannot exceed {1} characters.")]
        public string Password { get; set; } = null!;
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email Required")]
        public string Email { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Avatar { get; set; }
        public string Role { get; set; } = null!;
        public bool Status { get; set; }

        public virtual ICollection<Collection> Collections { get; set; }
        public virtual ICollection<MealPlanning> MealPlannings { get; set; }
        public virtual ICollection<Metadata> MetaData { get; set; }
    }
}
