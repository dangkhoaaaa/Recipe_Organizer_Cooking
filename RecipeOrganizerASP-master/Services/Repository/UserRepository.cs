using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Models.Authentication;
using Microsoft.EntityFrameworkCore;

namespace Services.Repository
{
    public class UserRepository : RepositoryBase<AppUser>
    {
        Recipe_OrganizerContext _context;
        DbSet<AppUser> _dbSet;
        public UserRepository()
        {
            _context = new Recipe_OrganizerContext();
            _dbSet = _context.Set<AppUser>();
        }

        
    }
}
