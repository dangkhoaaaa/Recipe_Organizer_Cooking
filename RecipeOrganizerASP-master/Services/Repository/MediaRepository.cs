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
		public void addMedia(string filePath)
		{
			Media media = new Media
			{
				Filelocation = filePath,
				Date = DateTime.Now
			};
			_dbSet.Add(media);
			_context.SaveChanges();
		}
	}
}
