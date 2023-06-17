using Microsoft.EntityFrameworkCore;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class SessionRepository: RepositoryBase<Session>
    {
        Recipe_OrganizerContext _context;

        DbSet<Session> _dbSet;

        public SessionRepository()
        {
            _context = new Recipe_OrganizerContext();
            _dbSet = _context.Set<Session>();
        }

        public List<Session> getSessionByDay(Day day)
        {
            var record = _dbSet.Where(s => s.DayId == day.DayId).ToList();
            return record;
        }
    }
}
