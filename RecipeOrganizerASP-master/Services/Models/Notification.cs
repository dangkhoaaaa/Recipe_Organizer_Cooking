using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public partial class Notification
    {
        public Notification()
        {
            MetaData = new HashSet<Metadata>();
        }

        public int Id { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime Date { get; set; }

        public virtual ICollection<Metadata> MetaData { get; set; }
    }
}
