using Firebase.Auth;
using Microsoft.EntityFrameworkCore;
using Services.Data;
using Services.Models;
using Services.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
		protected DbSet<Media> _dbSetMedia;
		protected DbSet<Feedback> _dbSet1;
        protected DbSet<AppUser> _dbSetAppUser;
        public FeedbackRepository()
        {
            _context = new Recipe_OrganizerContext();

            _dbSet = _context.Set<Feedback>();

            _dbSetFeedBack = _context.Set<Feedback>();
            _dbSetMetadata = _context.Set<Metadata>();
            _dbSet1 = _context.Set<Feedback>();
            _dbSetMedia = _context.Set<Media>();
            _dbSetAppUser = _context.Set<AppUser>(); 
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



		//public List<Feedback> getAllFeedBackByRecipe(int recipeId)
		//{
		//	List<Feedback> listmapCategory = new List<Feedback>();
		//	var query = from fb in _dbSetFeedBack
		//				join md in _dbSetMetadata on fb.FeedbackId equals md.FeedbackId
		//				where md.RecipeId == recipeId
		//				select fb;
  //          if(query.Count() > 0 )
  //          {
		//		listmapCategory = query.GroupBy(fb => fb.FeedbackId)
		//			   .Select(group => group.First())
		//			   .ToList();
		//	}
			
			
		//	return listmapCategory;
		//}


		public List<FeedBackOnOnceRecipeModel> getAllFeedBackByRecipe(int recipeId)
		{
			List<Feedback> listmapCategory = new List<Feedback>();
			List<FeedBackOnOnceRecipeModel> listresult = new List<FeedBackOnOnceRecipeModel>();
			var query = from fb in _dbSetFeedBack
						join md in _dbSetMetadata on fb.FeedbackId equals md.FeedbackId
						where md.RecipeId == recipeId
						select fb;
			listmapCategory = query.ToList();
			if (listmapCategory.Count() > 0)
			{
				FeedBackOnOnceRecipeModel feedBackOnOnceRecipeModel;
                foreach(var x in listmapCategory)
                {
					feedBackOnOnceRecipeModel = new FeedBackOnOnceRecipeModel();
					feedBackOnOnceRecipeModel.FeedbackId = x.FeedbackId;
                    feedBackOnOnceRecipeModel.Title = x.Title;
                    feedBackOnOnceRecipeModel.Date = x.Date;
                    feedBackOnOnceRecipeModel.Status = x.Status;
                    feedBackOnOnceRecipeModel.Description = x.Description;
                    feedBackOnOnceRecipeModel.Rating = x.Rating;

					var query1 = from fb1 in _dbSetFeedBack
								join md in _dbSetMetadata on fb1.FeedbackId equals md.FeedbackId
                                join us in _dbSetAppUser on md.UserId equals us.Id
								join me in _dbSetMedia on md.MediaId equals me.MediaId
								where md.RecipeId == recipeId && fb1.FeedbackId ==  x.FeedbackId
								select me.Filelocation;
				

					if(query1.Count() > 0 )
					{
						string url1 = query1.FirstOrDefault().ToString();
						feedBackOnOnceRecipeModel.Images = url1;
					}
                    

					var query2 = from fb1 in _dbSetFeedBack
								 join md in _dbSetMetadata on fb1.FeedbackId equals md.FeedbackId
								 join us in _dbSetAppUser on md.UserId equals us.Id
								 where md.RecipeId == recipeId && fb1.FeedbackId == x.FeedbackId
								 select us.FirstName;
					string url2 = query2.FirstOrDefault();
					if (query2 != null && query2.Count() > 0 && query2.Any()&& url2!=null && url2!="")
					{
						 url2 = url2.ToString();
						feedBackOnOnceRecipeModel.name = url2;
					}
					else
					{

						feedBackOnOnceRecipeModel.name = "Admin";
					}
					var query3 = from fb1 in _dbSetFeedBack
								 join md in _dbSetMetadata on fb1.FeedbackId equals md.FeedbackId
								 join us in _dbSetAppUser on md.UserId equals us.Id
								 where md.RecipeId == recipeId && fb1.FeedbackId == x.FeedbackId
								 select us.Image;
					string url3 = query3.FirstOrDefault();
					if (query3 != null && query3.Count() > 0 && query3.Any() && url3 != null && url3 != "")
					{
						url3 = url3.ToString();
						feedBackOnOnceRecipeModel.Avatar = url3;
					}
					else
					{

						feedBackOnOnceRecipeModel.Avatar = "https://firebasestorage.googleapis.com/v0/b/recipeorganizer-58fca.appspot.com/o/vector-users-icon.webp?alt=media&token=694d5c2a-4498-4a83-9a4a-d780d138d847";
					}

					//if (query3.Count() > 0)
					//{
					//	string url3 = query3.FirstOrDefault().ToString();
					//	feedBackOnOnceRecipeModel.Avatar = url3;
					//}
					listresult.Add(feedBackOnOnceRecipeModel);
				}
			
			}


			return listresult;
		}

		public List<UserFeedback> GetAllFeedbackUserWithMetadata(string userID)
		{
			var query = from m in _context.MetaData
						join r in _context.Recipes on m.RecipeId equals r.RecipeId
						join u in _context.Users on m.UserId equals u.Id
						join f in _context.Feedbacks on m.FeedbackId equals f.FeedbackId
						where u.Id == userID
						select new UserFeedback
						{
							User = u,
							Recipe = r,
							Feedback = f
						};
			return query.ToList();
		}

	}
}
