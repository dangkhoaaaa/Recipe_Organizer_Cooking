using Microsoft.EntityFrameworkCore;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class Parent_CategoryRepository : RepositoryBase<ParentCategory>
    {
        private new readonly Recipe_OrganizerContext _context;
        protected DbSet<ParentCategory> _dbSet1;
        public Parent_CategoryRepository()
        {
            _context = new Recipe_OrganizerContext();
            _dbSet1 = _context.Set<ParentCategory>();
        }
    }
}
