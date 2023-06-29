using Firebase.Auth;
using Microsoft.EntityFrameworkCore;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class FeedbackRepository : RepositoryBase<Feedback>
    {
        Recipe_OrganizerContext _context;

        protected DbSet<Feedback> _dbSet;
        public object feedbackId;

        protected DbSet<Feedback> _dbSetFeedBack;
        protected DbSet<Metadata> _dbSetMetadata;

        protected DbSet<Feedback> _dbSet1;

        public FeedbackRepository()
        {
            _context = new Recipe_OrganizerContext();

            _dbSet = _context.Set<Feedback>();

            _dbSetFeedBack = _context.Set<Feedback>();
            _dbSetMetadata = _context.Set<Metadata>();
            _dbSet1 = _context.Set<Feedback>();

        }

        public ICollection<Feedback> Products { get; set; } = new List<Feedback>();
        
        public double valueAvgRateRecipe(int recipeId)
        {
            List<Feedback> listmapCategory = new List<Feedback>();
            var query = from fb in _dbSetFeedBack
                        join md in _dbSetMetadata on fb.FeedbackId equals md.FeedbackId
                        where md.RecipeId == recipeId
                        select fb;

            listmapCategory = query.DistinctBy( r => r.FeedbackId).ToList();
            double result = 0;
            if (listmapCategory.Count > 0 )
            {

                double totalRate = 0;
                foreach (var item in listmapCategory)
                {
                    totalRate += item.Rating;
                }
                 result = totalRate / listmapCategory.Count;
            }
            return result;
        }

		public List<Feedback> GetByFeedbackByUser(string userId)
		{
			List<Feedback> feebackList = _dbSet.Where(r => r.MetaData.Any(md => md.UserId == userId && 
                                                                                md.RecipeId != null && 
                                                                                md.FeedbackId != null)).ToList();
			return feebackList;
		}


	}
}
