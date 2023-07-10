using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Data
{
	public class ShowContact
	{
		public Contact Contact { get; set; }
		public List<int> ErrorNum { get; set; }
	}
}
