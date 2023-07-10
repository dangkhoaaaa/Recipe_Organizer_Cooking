using Services.Models.Authentication;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Data
{
	public class UserFeedback
	{
		public AppUser User { get; set; }
		public Recipe Recipe { get; set; }
		public Feedback Feedback { get; set; }
	}
}
