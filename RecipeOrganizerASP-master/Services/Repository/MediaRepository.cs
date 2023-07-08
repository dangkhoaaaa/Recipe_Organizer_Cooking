using Microsoft.AspNetCore.Http;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
	public class MediaRepository : RepositoryBase<Media>
	{
		public int addMedia(string filePath)
		{
			Media media = new Media
			{
				Filelocation = filePath,
				Date = DateTime.Now
			};
			_dbSet.Add(media);
			_context.SaveChanges();

			return media.MediaId;
		}

		public List<Media> GetImgsByFeedbackId(int feedbackId)
		{
			List<Media> medias = new List<Media>();

			if (feedbackId != 0)
			{
				var metaDataList = _dbSet.Where(r => r.MetaData.Any(md => md.FeedbackId == feedbackId)).ToList();

				foreach (var metaData in metaDataList)
				{
					var media = _dbSet.Find(metaData.MediaId);
					if (media != null)
					{
						medias.Add(media);
					}
				}
			}

			return medias;
		}

		public List<Media> GetImgsByRecipeId(int recipeId)
		{
			List<Media> medias = new List<Media>();

			if (recipeId != 0)
			{
				var metaDataList = _dbSet.Where(r => r.MetaData.Any(md => md.RecipeId == recipeId)).ToList();

				foreach (var metaData in metaDataList)
				{
					var media = _dbSet.Find(metaData.MediaId);
					if (media != null)
					{
						medias.Add(media);
					}
				}
			}

			return medias;
		}

	}
}
