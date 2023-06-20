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
<<<<<<< HEAD
        protected DbSet<Feedback> _dbSet1;
        public object feedbackId;
=======

        protected DbSet<Feedback> _dbSetFeedBack;
        protected DbSet<Metadata> _dbSetMetadata;
>>>>>>> 176a703046f2002d8656a3231c7ab22a34c0a480

        public FeedbackRepository()
        {
            _context = new Recipe_OrganizerContext();
<<<<<<< HEAD
            _dbSet1 = _context.Set<Feedback>();
        }

        public ICollection<Feedback> Products { get; set; } = new List<Feedback>();

        //public List<Feedback> getFeedbackByUserId(string userId)
        //{
        //    List<Feedback> listFeedback = new List<Feedback>();

        //    if (userId != null && userId.Trim().Length > 0)
        //    {
        //        foreach (var Feedback in _dbSet)
        //        {
        //            // Check if the feedback is associated with the specified user
        //            var metadata = Metadata(m => m.feedback_id == Feedback.feedback_id && m.user_id == userId);
        //            if (metadata != null)
        //            {
        //                listFeedback.Add(Feedback);
        //            }
        //        }
        //    }

        //    return listFeedback;
        //}
=======
            _dbSetFeedBack = _context.Set<Feedback>();
            _dbSetMetadata = _context.Set<Metadata>();
        }
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
>>>>>>> 176a703046f2002d8656a3231c7ab22a34c0a480
    }
}
