using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeOrganizerTest
{
    public class BaseTest<T> where T : class
    {
        protected Recipe_OrganizerContext _context;
        protected DbSet<T> _dbSet;

        [SetUp]
        public void BaseSetUp()
        {
            _context = new Recipe_OrganizerContext();
            _dbSet = _context.Set<T>();
        }

        [TearDown]
        public void BaseTearDown()
        {
          _context.Dispose();
        }
    }
}
