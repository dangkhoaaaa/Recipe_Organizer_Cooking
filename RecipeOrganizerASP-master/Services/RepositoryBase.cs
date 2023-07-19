using Microsoft.EntityFrameworkCore;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RepositoryBase<T> where T : class
    {
        protected Recipe_OrganizerContext _context;
		protected DbSet<T> _dbSet;

        public RepositoryBase()
        {
            _context = new Recipe_OrganizerContext();
            _dbSet = _context.Set<T>();
        }


		public List<T> SearchByProperty(Expression<Func<T, bool>> predicate)
		{
			return _dbSet.Where(predicate).ToList();
		}

		public List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
            _context.SaveChanges();
        }

        public bool Delete(T entity)
        {
            try
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return false;
            }
            return true;
        }
    }
}
