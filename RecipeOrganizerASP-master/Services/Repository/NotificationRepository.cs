using Microsoft.EntityFrameworkCore;
using Services.Data;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class NotificationRepository : RepositoryBase<Notification>
    {
        Recipe_OrganizerContext _context;
        protected DbSet<Notification> _dbSet;

        public NotificationRepository()
        {
            _context = new Recipe_OrganizerContext();
            _dbSet = _context.Set<Notification>();
        }

        public Notification GetNotification(int noti)
        {
            return GetAll().Where(p => p.Id == noti).FirstOrDefault();
        }
        public int addNotification(string message)
        {
            Notification notification = new Notification
            {
                Message = message,
                IsRead = false,
                Date = DateTime.Now,
            };
            Add(notification);
            return notification.Id;
        }

        public void updateIsRead(Notification notification)
        {
            if (!notification.IsRead)
            {
                notification.IsRead = true;
                Update(notification);
            }
        }

        public List<RecipeNotification> GetAllNofiticationWithMetadata()
        {
            var query = from m in _context.MetaData
                        join r in _context.Recipes on m.RecipeId equals r.RecipeId
                        join u in _context.Users on m.UserId equals u.Id
                        join n in _context.Notifications on m.NotificationId equals n.Id
                        select new RecipeNotification
                        {
                            user = u,
                            recipe = r,
                            notification = n
                        };
            return query.ToList();
        }

        public List<RecipeNotification> GetNofiticationWithIsReadWithMetada(bool IsRead)
        {
            var query = from m in _context.MetaData
                        join r in _context.Recipes on m.RecipeId equals r.RecipeId
                        join u in _context.Users on m.UserId equals u.Id
                        join n in _context.Notifications on m.NotificationId equals n.Id
                        where n.IsRead == IsRead
                        select new RecipeNotification
                        {
                            user = u,
                            recipe = r,
                            notification = n
                        };
            return query.ToList();
        }

        //public RecipeNotification GetNofiticationWithRecipe(int recipeID)
        //{
        //    var query = from m in _context.MetaData
        //                join r in _context.Recipes on m.RecipeId equals r.RecipeId
        //                join u in _context.Users on m.UserId equals u.Id
        //                join n in _context.Notifications on m.NotificationId equals n.Id
        //                where r.RecipeId == recipeID
        //                select new RecipeNotification
        //                {
        //                    user = u,
        //                    recipe = r,
        //                    notification = n
        //                };
        //    RecipeNotification noti = query.FirstOrDefault();
        //    return noti;
        //}

        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
