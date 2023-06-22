using Microsoft.EntityFrameworkCore;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
	public class MetadataRepository : RepositoryBase<Metadata>
	{
		Recipe_OrganizerContext _context;
		protected DbSet<Metadata> _dbSet;
		

		protected DbSet<Feedback> _dbSetFeedBack;
		protected DbSet<Metadata> _dbSetMetadata;

		public MetadataRepository()
		{
			_context = new Recipe_OrganizerContext();
			_dbSet = _context.Set<Metadata>();
		}

		public ICollection<Metadata> Products { get; set; } = new List<Metadata>();
	}
}
