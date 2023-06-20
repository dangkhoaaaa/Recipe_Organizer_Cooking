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
        protected DbSet<Feedback> _dbSet1;
        public object feedbackId;

        public FeedbackRepository()
        {
            _context = new Recipe_OrganizerContext();
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
    }
}
