

namespace RecipeOrganizer.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class User
    {
        public User()
        {
            this.Collections = new HashSet<Collection>();
            this.Feedbacks = new HashSet<Feedback>();
            this.MealPlannings = new HashSet<MealPlanning>();
            this.Media = new HashSet<Media>();
            this.User_Like_Recipe = new HashSet<User_Like_Recipe>();
        }

        [Key]
        [Required]
        [Column("user_id", Order = 1)]
        public int UserID { get; set; }
        [MaxLength(50)]
        [Required]
        [Column("username", Order = 2)]
        public string Username { get; set; }
        [MaxLength(50)]
        [Required]
        [Column("password", Order = 3)]
        public string Password { get; set; }
        [MaxLength(50)]
        [Required]
        [Column("email", Order = 4)]
        public string Email { get; set; }
        [MaxLength(50)]
        [Column("firstname", Order = 5)]
        public string Firstname { get; set; }
        [MaxLength(50)]
        [Column("lastname", Order = 6)]
        public string Lastname { get; set; }
        
        [Column("birthday", Order = 7)]
        public Nullable<System.DateTime> Birthday { get; set; }
        [Column("avatar", Order = 8)]
        [MaxLength(2048)]
        public string Avatar { get; set; }
        [Column("status", Order = 9)]
        public Nullable<bool> Status { get; set; }
    
        public virtual ICollection<Collection> Collections { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<MealPlanning> MealPlannings { get; set; }
        public virtual ICollection<Media> Media { get; set; }
        public virtual ICollection<User_Like_Recipe> User_Like_Recipe { get; set; }
    }
}
