//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RecipeOrganizer.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class MealPlanning
    {
        [Key]
        public int plan_id { get; set; }
        [ForeignKey("User")]
        public int user_id { get; set; }
        public System.DateTime week_start_date { get; set; }
        public int session_recipe_id { get; set; }
    
        public virtual User User { get; set; }
    }
}
