using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Models;
using Services.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class MealPlaningRepository : RepositoryBase<MealPlanning>
    {

        Recipe_OrganizerContext _context;

        protected DbSet<MealPlanning> _dbSet;




        public MealPlaningRepository()
        {
            _context = new Recipe_OrganizerContext();
            _dbSet = _context.Set<MealPlanning>();
        }

        public MealPlanning GetPlanID(string UserID, string Week)
        {
            MealPlanning meal = _dbSet.Where(m => m.UserId.Equals(UserID) && m.WeekStartDate.Equals(Week)).FirstOrDefault();


            return meal;
        }
        public Slot showPlan(string week, string userID)
        {
            DayRepository _dayRepository = new DayRepository();     
            Slot slot = new Slot();
            MealPlanning meal = GetPlanID(userID, week);
            if (meal != null)
            {
                slot = _dayRepository.showDay(meal);
                if (slot == null)
                {
                    Delete(meal);
                }
                return slot;
            }
            
            return slot;
           

        }

        public string WeekNow()
        {
            // Get the current date
            DateTime currentDate = DateTime.Today;

            // Get the calendar week number
            int currentWeek = GetWeekOfYear(currentDate);

            // Format the output as "year-Wweek"
            string formattedOutput = $"{currentDate.Year}-W{currentWeek:D2}";

            return formattedOutput;
        }

        public int GetWeekOfYear(DateTime date)
        {
            // Determine the week number based on ISO 8601 standard
            return System.Globalization.CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date,
                System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public void AddPlan(List<CartLine> cartLines, string week, string userID)
        {
            DayRepository _dayRepository = new DayRepository();

            MealPlanning meal = _dbSet.Where(m => m.UserId.Equals(userID) && m.WeekStartDate.Equals(week)).FirstOrDefault();
            if (cartLines.Count == 0)
            {
                if (meal != null)
                {
                    if (_dayRepository.RemoveDay(meal))
                    {
                        Delete(meal);
                    }
                    
                }
            }
            else
            {
                if (meal == null)
                {
                    meal = new MealPlanning
                    {
                        UserId = userID,
                        WeekStartDate = week

                    };
                    _dbSet.Add(meal);
                    _context.SaveChanges();

                }
                _dayRepository.addDay(cartLines, meal);
            }
            
            //MealPlanning meal = _dbSet.Where(m => m.UserId.Equals(userID) && m.WeekStartDate.Equals(week)).FirstOrDefault();
            
           
        }

    }
}
