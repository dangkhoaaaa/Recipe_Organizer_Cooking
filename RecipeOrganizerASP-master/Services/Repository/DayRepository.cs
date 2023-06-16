using Microsoft.EntityFrameworkCore;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class DayRepository : RepositoryBase<Day>
    {
        Recipe_OrganizerContext _context;

        DbSet<Day> _dbSet;

        public DayRepository()
        {
            _context = new Recipe_OrganizerContext();
            _dbSet = _context.Set<Day>();
        }

        public List<Day> getDayByPlan(MealPlanning meal)
        {
            var record = _dbSet.Where(d => d.PlanId == meal.PlanId).ToList();
            return record;
        }
    }
}
