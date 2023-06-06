using System;
using System.Collections.Generic;

namespace Services.Models
{
    public partial class Session
    {
        public int SessionId { get; set; }
        public int DayId { get; set; }
        public string SessionName { get; set; } = null!;

        public virtual Day Day { get; set; } = null!;
    }
}
