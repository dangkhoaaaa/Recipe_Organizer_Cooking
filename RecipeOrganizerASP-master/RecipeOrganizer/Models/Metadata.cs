using System;
using System.Collections.Generic;

namespace RecipeOrganizer.Models
{
    public partial class Metadata
    {
        public int MetadataId { get; set; }
        public int UserId { get; set; }
        public int RecipeId { get; set; }
        public int MediaId { get; set; }
        public int FeedbackId { get; set; }

        public virtual Feedback Feedback { get; set; } = null!;
        public virtual Media Media { get; set; } = null!;
        public virtual Recipe Recipe { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
