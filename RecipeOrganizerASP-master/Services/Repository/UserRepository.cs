using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Models.Authentication;
using Microsoft.EntityFrameworkCore;
using Services.Data;
using System.Threading;

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

        public AppUser GetUserById(string userID) {
            return GetAll().Where(p => p.Id == userID).FirstOrDefault();
        }
        public AppUser GetUserByRecipe(int recipeID) {
            var query = from m in _context.MetaData
                        join r in _context.Recipes on m.RecipeId equals r.RecipeId
                        join u in _context.Users on m.UserId equals u.Id
                        where r.RecipeId == recipeID
                        select new AppUser
                        {
                            Id = u.Id
                        };
            return query.FirstOrDefault();
        }
    }
}
