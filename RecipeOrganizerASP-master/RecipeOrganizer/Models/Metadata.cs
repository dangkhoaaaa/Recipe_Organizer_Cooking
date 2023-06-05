using System;
using System.Collections.Generic;

namespace RecipeOrganizer.Models
{
    public partial class Metadata
    {
        public int MetadataId { get; set; }
        public int? UserId { get; set; }
        public int? RecipeId { get; set; }
        public int? MediaId { get; set; }
        public int? FeedbackId { get; set; }

        public virtual Feedback? Feedback { get; set; }
        public virtual Media? Media { get; set; }
        public virtual Recipe? Recipe { get; set; }
        public virtual User? User { get; set; }
    }
}
