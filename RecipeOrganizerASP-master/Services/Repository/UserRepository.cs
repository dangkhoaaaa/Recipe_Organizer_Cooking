using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Models.Authentication;
using Microsoft.EntityFrameworkCore;

namespace Services.Repository
{
    public class UserRepository : RepositoryBase<AppUser>
    {
        Recipe_OrganizerContext _context;
        DbSet<AppUser> _dbSet;
        public UserRepository()
        {
            _context = new Recipe_OrganizerContext();
            _dbSet = _context.Set<AppUser>();
        }

        public List<AppUser> getAccountByName(string userName)
        {
            var records = _dbSet.Where(entity => entity.UserName.Contains(userName)).ToList();
            return records;
        }
        //cái này demo bên winform. tham khảo thôi nha
        public void Add(AppUser AppUser)
        {
            var lastRecord = _dbSet.OrderByDescending(entity => entity.Id).FirstOrDefault();
            if (lastRecord != null)
            {
                AppUser.Id = autoGenerateID(lastRecord.Id);
            }
            else
            {
                AppUser.Id = autoGenerateID("ACCT0000");
            }
            _dbSet.Add(AppUser);
            _context.SaveChanges();
        }

        public string autoGenerateID(string id)
        {
            //ACCT0001
            string result = "";
            int cutID = int.Parse(id.Substring(4, 4));
            cutID++;
            int digits = 4;
            string prefix = "ACCT";

            // Convert the current ID to string with leading zeros
            string idString = cutID.ToString().PadLeft(digits, '0');
            result = prefix + idString;

            // Combine the prefix and the formatted ID
            return result;
        }
    }
}
