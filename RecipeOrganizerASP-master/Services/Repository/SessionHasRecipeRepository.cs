using Microsoft.EntityFrameworkCore;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class SessionHasRecipeRepository: RepositoryBase<SessionHasRecipeRepository>
    {
        Recipe_OrganizerContext _context;

        DbSet<SessionHasRecipe> _dbSet;

        public SessionHasRecipeRepository()
        {
            _context = new Recipe_OrganizerContext();
            _dbSet = _context.Set<SessionHasRecipe>();
        }

        public List<SessionHasRecipe> getSessionHasRecipeBySession(Session session)
        {
            var record = _dbSet.Where(sr => sr.SessionId == session.SessionId).ToList();
            return record;
        }
    }
}
