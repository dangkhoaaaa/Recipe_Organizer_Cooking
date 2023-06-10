using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
	public class TagRepository : RepositoryBase<Tag>
	{
		public Tag? GetByName(string tagName)
		{
			return _dbSet.FirstOrDefault(t => t.TagName == tagName);
		}
	}
}
