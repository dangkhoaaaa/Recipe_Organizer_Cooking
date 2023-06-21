using Microsoft.EntityFrameworkCore;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class MealPlaningRepository: RepositoryBase<MealPlanning>
    {

        Recipe_OrganizerContext _context;

        protected DbSet<MealPlanning> _dbSet;

		

		public MealPlaningRepository()
        {
            _context = new Recipe_OrganizerContext();
            _dbSet = _context.Set<MealPlanning>();
        }

        public MealPlanning GetPlanID(string UserID,  string Week)
        {
            MealPlanning meal = _dbSet.Where(m =>  m.UserId.Equals(UserID) && m.WeekStartDate.Equals(Week)).FirstOrDefault();
            
            
            return meal;
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

        public int  GetWeekOfYear(DateTime date)
        {
            // Determine the week number based on ISO 8601 standard
            return System.Globalization.CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date,
                System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }


    }
}
