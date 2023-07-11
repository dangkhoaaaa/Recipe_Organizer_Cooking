using Microsoft.EntityFrameworkCore;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class ContactFormModel : RepositoryBase<Contact>
    {
        private new readonly Recipe_OrganizerContext _context;

        DbSet<Contact> _dbSet;

        public ContactFormModel()
        {
            _context = new Recipe_OrganizerContext();
            _dbSet = _context.Set<Contact>();

        }

        public List<int> CheckForm(Contact contact)
        {
            var l = new List<int>();
            if (contact == null)
            {
                return new List<int>() { 0 };
            }
            else
            {
                if (contact.Name == null || contact.Name.Length < 1 || contact.Name.Length > 100)
                {
                    l.Add(1);
                }
                if (contact.Email == null || contact.Email.Length < 1 || contact.Email.Length >= 250)
                {
                    l.Add(2);
                }
                if (contact.Address != null && contact.Address.Length > 300)
                {
                    l.Add(3);
                }
                else if (contact.Address == null)
                {
                    contact.Address = "";
                }
                if (contact.Message == null || contact.Message.Length < 1 || contact.Message.Length > 1000)
                {
                    l.Add(4);
                }

            }
            if (l.Count == 0)
            {
                contact.Date = DateTime.Now;
                Add(contact);
                contact = null;
                return l;
            }
            return l;

        }
    }

}
