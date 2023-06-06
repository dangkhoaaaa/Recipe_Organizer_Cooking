using System;
using System.Collections.Generic;

namespace Services.Models
{
    public partial class Tag
    {
        public int TagId { get; set; }
        public string TagName { get; set; } = null!;
    }
}
