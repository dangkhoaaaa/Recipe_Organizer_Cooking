using System;
using System.Collections.Generic;

namespace RecipeOrganizer.Models
{
    public partial class Media
    {
        public Media()
        {
            MetaData = new HashSet<Metadata>();
        }

        public int MediaId { get; set; }
        public string Filelocation { get; set; } = null!;
        public DateTime Date { get; set; }

        public virtual ICollection<Metadata> MetaData { get; set; }
    }
}
