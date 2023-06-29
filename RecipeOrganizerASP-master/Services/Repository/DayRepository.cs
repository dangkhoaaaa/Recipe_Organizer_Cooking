using Microsoft.EntityFrameworkCore;
using Services.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class DayRepository : RepositoryBase<Day>
    {
        Recipe_OrganizerContext _context;

        DbSet<Day> _dbSet;

		

		public DayRepository()
        {
            _context = new Recipe_OrganizerContext();
            _dbSet = _context.Set<Day>();
        }

        public List<Day> getDayByPlan(MealPlanning meal)
        {
            var record = _dbSet.Where(d => d.PlanId == meal.PlanId).ToList();
           
            return record;
        }
        public Slot showDay(MealPlanning meal)
        {
            SessionRepository _sessionRepository = new SessionRepository();
            //DayRepository _dayRepository = new DayRepository();
            var days = getDayByPlan(meal);
            Slot slot = new Slot();
            List<Day> daysList = new List<Day>();
            foreach (Day d in days)
            {
                
                switch (d.DayOfWeek)
                {
                    case "Mon":
                        {
                            if (!slot.AddSlot(_sessionRepository.showSessions(meal, 0, d))) 
                            { 
                                daysList.Add(d);
                            }
                            break;
                        }
                    case "Tue":
                        {
                            if (!slot.AddSlot(_sessionRepository.showSessions(meal, 1, d)))
                            {
                                daysList.Add(d);
                            }
                            break;
                        }
                    case "Wed":
                        {
                            if (!slot.AddSlot(_sessionRepository.showSessions(meal, 2, d)))
                            {
                                daysList.Add(d);
                            }
                            break;
                        }
                    case "Thu":
                        {
                            if (!slot.AddSlot(_sessionRepository.showSessions(meal, 3, d)))
                            {
                                daysList.Add(d);
                            }
                            break;
                        }
                    case "Fri":
                        {
                            if (!slot.AddSlot(_sessionRepository.showSessions(meal, 4, d)))
                            {
                                daysList.Add(d);
                            }
                            break;
                        }
                    case "Sat":
                        {
                            if (!slot.AddSlot(_sessionRepository.showSessions(meal, 5, d)))
                            {
                                daysList.Add(d);
                            }
                            break;
                        }
                    default:
                        {
                            if (!slot.AddSlot(_sessionRepository.showSessions(meal, 6, d)))
                            {
                                daysList.Add(d);
                            }
                            break;
                        }
                }
                
            }
            if (daysList.Count > 0)
            {
                foreach (var d in daysList)
                {
                    Delete(d);
                }
            }
            return slot;
        }
        public bool RemoveDay(MealPlanning meal)
        {
            SessionRepository _sessionRepository = new SessionRepository();
            //DayRepository _dayRepository = new DayRepository();
            var days = getDayByPlan(meal);
            

            foreach (Day d in days)
            {

                switch (d.DayOfWeek)
                {
                    case "Mon":
                        {
                            if (_sessionRepository.RemoveSession(d))
                            {
                                Delete(d);
                            }
                            break;
                        }
                    case "Tue":
                        {
                            if (_sessionRepository.RemoveSession(d))
                            {
                                Delete(d);
                            }
                            break;
                        }
                    case "Wed":
                        {
                            if (_sessionRepository.RemoveSession(d))
                            {
                                Delete(d);
                            }
                            break;
                        }
                    case "Thu":
                        {
                            if (_sessionRepository.RemoveSession(d))
                            {
                                Delete(d);
                            }
                            break;
                        }
                    case "Fri":
                        {
                            if (_sessionRepository.RemoveSession(d))
                            {
                                Delete(d);
                            }
                            break;
                        }
                    case "Sat":
                        {
                            if (_sessionRepository.RemoveSession(d))
                            {
                                Delete(d);
                            }
                            break;
                        }
                    default:
                        {
                            if (_sessionRepository.RemoveSession(d))
                            {
                                Delete(d);
                            }
                            break;
                        }
                }

            }
            return true;
        }
        public bool SaveDay(List<CartLine> cartLines, MealPlanning meal)
        {
            SessionRepository _sessionRepository = new SessionRepository();
            //DayRepository _dayRepository = new DayRepository();
            var days = getDayByPlan(meal);

            List<Day> dayList = new List<Day>();
            foreach (Day d in days)
            {
                List<CartLine> lines = new List<CartLine>();
                switch (d.DayOfWeek)
                {
                    case "Mon":
                        {
                            foreach (var line in cartLines)
                            {
                                if ((line.SlotID - 1) / 3 == 0)
                                {
                                    lines.Add(line);
                                     
                                }
                            }
                            if (lines.Count > 0)
                            {
                                
                                foreach (var line in lines) { 
                                    cartLines.Remove(line);
                                    
                                }
                                _sessionRepository.SaveSession(lines, d);

                            } else {
                                if (_sessionRepository.RemoveSession(d))
                                {
                                    Delete(d);
                                }
                            }
                            
                            break;
                        }
                    case "Tue":
                        {
                            foreach (var line in cartLines)
                            {
                                if ((line.SlotID - 1) / 3 == 1)
                                {
                                    lines.Add(line);
                                   
                                }
                            }
                            if (lines.Count > 0)
                            {
                                
                                foreach (var line in lines)
                                {
                                    cartLines.Remove(line);
                                }
                                _sessionRepository.SaveSession(lines, d);
                            }
                            else
                            {
                                if (_sessionRepository.RemoveSession(d))
                                {
                                    Delete(d);
                                }
                            }
                            break;
                        }
                    case "Wed":
                        {
                            foreach (var line in cartLines)
                            {
                                if ((line.SlotID - 1) / 3 == 2)
                                {
                                    lines.Add(line);
                                    
                                }
                            }
                            if (lines.Count > 0)
                            {
                                
                                foreach (var line in lines)
                                {
                                    cartLines.Remove(line);
                                }
                                _sessionRepository.SaveSession(lines, d);
                            }
                            else
                            {
                                if (_sessionRepository.RemoveSession(d))
                                {
                                    Delete(d);
                                }
                            }
                            break;
                        }
                    case "Thu":
                        {
                            foreach (var line in cartLines)
                            {
                                if ((line.SlotID - 1) / 3 == 3)
                                {
                                    lines.Add(line);
                                    
                                }
                            }
                            if (lines.Count > 0)
                            {
                                
                                foreach (var line in lines)
                                {
                                    cartLines.Remove(line);
                                }
                                _sessionRepository.SaveSession(lines, d);
                            }
                            else
                            {
                                if (_sessionRepository.RemoveSession(d))
                                {
                                    Delete(d);
                                }
                            }
                            break;
                        }
                    case "Fri":
                        {
                            foreach (var line in cartLines)
                            {
                                if ((line.SlotID - 1) / 3 == 4)
                                {
                                    lines.Add(line);
                                    
                                }
                            }
                            if (lines.Count > 0)
                            {
                                
                                foreach (var line in lines)
                                {
                                    cartLines.Remove(line);
                                }
                                _sessionRepository.SaveSession(lines, d);
                            }
                            else
                            {
                                if (_sessionRepository.RemoveSession(d))
                                {
                                    Delete(d);
                                }
                            }
                            break;
                        }
                    case "Sat":
                        {
                            foreach (var line in cartLines)
                            {
                                if ((line.SlotID - 1) / 3 == 5)
                                {
                                    lines.Add(line);
                                    
                                }
                            }
                            if (lines.Count > 0)
                            {
                                
                                foreach (var line in lines)
                                {
                                    cartLines.Remove(line);
                                }
                                _sessionRepository.SaveSession(lines, d);
                            }
                            else
                            {
                                if (_sessionRepository.RemoveSession(d))
                                {
                                    Delete(d);
                                }
                            }
                            break;
                        }
                    default:
                        {
                            foreach (var line in cartLines)
                            {
                                if ((line.SlotID - 1) / 3 == 6)
                                {
                                    lines.Add(line);
                                    
                                }
                            }
                            if (lines.Count > 0)
                            {
                                
                                foreach (var line in lines)
                                {
                                    cartLines.Remove(line);
                                }
                                _sessionRepository.SaveSession(lines, d);
                            }
                            else
                            {
                                if (_sessionRepository.RemoveSession(d))
                                {
                                    Delete(d);
                                }
                            }
                            break;
                        }
                }

            }
            if (cartLines.Count > 0)
            {
                addDay(cartLines, meal);
            }
            //if (dayList.Count > 0)
            //{
            //    foreach (var day in dayList)
            //    {
            //        Delete(day);
            //    }
            //}
            return true;
        }
        public void addDay(List<CartLine> cartLines, MealPlanning meal)
        {
            List<Day> days = _dbSet.Where(d => d.PlanId == meal.PlanId).ToList();
            Day newDay = null;
            SessionRepository _sessionRepository = new SessionRepository();
            
            foreach (CartLine cartLine in cartLines)
            {
                var count = 0;
                switch ((cartLine.SlotID - 1) / 3) {
                    case 0:
                        {   
                            foreach (Day day in days)
                            {
                                if (day.DayOfWeek == "Mon")
                                {
                                    count++;
                                    _sessionRepository.addSession(cartLine, day);
                                    break;
                                } 
                            }
                            if (count != 0) {
                                break;
                            } else {
                            newDay = new Day { 
                            DayOfWeek = "Mon",
                            PlanId = meal.PlanId
                            };
                            _dbSet.Add(newDay);
                            _context.SaveChanges();
                            _sessionRepository.addSession(cartLine, newDay);
                            }
                            break;
                        }
                    case 1:
                        {
                            foreach (Day day in days)
                            {
                                if (day.DayOfWeek == "Tue")
                                {
                                    count ++;
                                    _sessionRepository.addSession(cartLine, day);
                                    break;
                                }
                            }
                            if (count != 0)
                            {
                                break;
                            }
                            else
                            {

                                newDay = new Day
                                {
                                    DayOfWeek = "Tue",
                                    PlanId = meal.PlanId
                                };
                                _dbSet.Add(newDay);
                                _context.SaveChanges();
                                _sessionRepository.addSession(cartLine, newDay);
                            }
                            break;
                        }
                    case 2:
                        {
                            foreach (Day day in days)
                            {
                                if (day.DayOfWeek == "Wed")
                                {
                                    count++;
                                    _sessionRepository.addSession(cartLine, day);
                                    break;
                                }
                            }
                            if (count != 0)
                            {
                                break;
                            }
                            else
                            {
                                newDay = new Day
                                {
                                    DayOfWeek = "Wed",
                                    PlanId = meal.PlanId
                                };
                                _dbSet.Add(newDay);
                                _context.SaveChanges();
                                _sessionRepository.addSession(cartLine, newDay);
                            }
                            break;
                        }
                    case 3:
                        {
                            foreach (Day day in days)
                            {
                                if (day.DayOfWeek == "Thu")
                                {
                                    count++;
                                    _sessionRepository.addSession(cartLine, day);
                                    break;
                                }
                            }
                            if (count != 0)
                            {
                                break;
                            }
                            else
                            {
                                newDay = new Day
                                {
                                    DayOfWeek = "Thu",
                                    PlanId = meal.PlanId
                                };
                                _dbSet.Add(newDay);
                                _context.SaveChanges();
                                _sessionRepository.addSession(cartLine, newDay);
                            }
                            break;
                        }
                    case 4:
                        {
                            foreach (Day day in days)
                            {
                                if (day.DayOfWeek == "Fri")
                                {
                                    count++;
                                    _sessionRepository.addSession(cartLine, day);
                                    break;
                                }
                            }
                            if (count != 0)
                            {
                                break;
                            }
                            else
                            {
                                newDay = new Day
                                {
                                    DayOfWeek = "Fri",
                                    PlanId = meal.PlanId
                                };
                                _dbSet.Add(newDay);
                                _context.SaveChanges();
                                _sessionRepository.addSession(cartLine, newDay);
                            }
                            break;
                        }
                    case 5:
                        {
                            foreach (Day day in days)
                            {
                                if (day.DayOfWeek == "Sat")
                                {
                                    count++;
                                    _sessionRepository.addSession(cartLine, day);
                                    break;
                                }
                            }
                            if (count != 0)
                            {
                                break;
                            }
                            else
                            {
                                newDay = new Day
                                {
                                    DayOfWeek = "Sat",
                                    PlanId = meal.PlanId
                                };
                                _dbSet.Add(newDay);
                                _context.SaveChanges();
                                _sessionRepository.addSession(cartLine, newDay);
                            }
                            break;
                        }
                    default:
                        {
                            foreach (Day day in days)
                            {
                                if (day.DayOfWeek == "Sun")
                                {
                                    count++;
                                    _sessionRepository.addSession(cartLine, day);
                                    break;
                                }
                            }
                            if (count != 0)
                            {
                                break;
                            }
                            else
                            {
                                newDay = new Day
                                {
                                    DayOfWeek = "Sun",
                                    PlanId = meal.PlanId
                                };
                                _dbSet.Add(newDay);
                                _context.SaveChanges();
                                _sessionRepository.addSession(cartLine, newDay);
                            }
                            break;
                        }
                    
                }
            }
        }
        
    }
}
