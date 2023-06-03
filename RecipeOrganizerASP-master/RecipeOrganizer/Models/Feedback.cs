using System;
using System.Collections.Generic;

namespace RecipeOrganizer.Models
{
    public partial class Feedback
    {
        public Feedback()
        {
            MetaData = new HashSet<Metadata>();
        }

        public int FeedbackId { get; set; }
        public string Description { get; set; } = null!;
        public string Title { get; set; } = null!;
        public DateTime Date { get; set; }
        public int? Rating { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<Metadata> MetaData { get; set; }
    }
}
