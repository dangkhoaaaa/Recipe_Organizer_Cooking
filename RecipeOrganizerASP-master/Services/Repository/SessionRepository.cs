using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class SessionRepository : RepositoryBase<Session>
    {
        Recipe_OrganizerContext _context;

        DbSet<Session> _dbSet;

        public SessionRepository()
        {
            _context = new Recipe_OrganizerContext();
            _dbSet = _context.Set<Session>();
        }

        public List<Session> getSessionByDay(Day day)
        {
            var record = _dbSet.Where(s => s.DayId == day.DayId).ToList();
            return record;
        }

        public void addSession(CartLine cartLine, Day day)
        {
            List<Session> sessions = _dbSet.Where(s => s.DayId == day.DayId).ToList();
            Session session = null;
            SessionHasRecipeRepository _sessionHasRecipeRepository = new SessionHasRecipeRepository();
            
            var count = 0;
            switch (cartLine.SlotID % 3)
            {
                case 1:
                    {
                        foreach (Session session1 in sessions)
                        {
                            if (session1.SessionName == "breakfast")
                            {
                                count++;
                                _sessionHasRecipeRepository.addRecipeToSession(cartLine, session1 as Session);
                                break;
                            }
                        }
                        if (count != 0)
                        {
                            break;
                        }
                        else
                        {
                            session = new Session
                            {
                                SessionName = "breakfast",
                                DayId = day.DayId
                            };
                            _dbSet.Add(session);
                            _context.SaveChanges();
                            _sessionHasRecipeRepository.addRecipeToSession(cartLine, session as Session);
                        }
                        break;
                    }
                case 2:
                    {
                        foreach (Session session1 in sessions)
                        {
                            if (session1.SessionName == "lunch")
                            {
                                count++;
                                _sessionHasRecipeRepository.addRecipeToSession(cartLine, session1 as Session);
                                break;
                            }
                        }
                        if (count != 0)
                        {
                            break;
                        }
                        else
                        {
                            session = new Session
                            {
                                SessionName = "lunch",
                                DayId = day.DayId
                            };
                            _dbSet.Add(session);
                            _context.SaveChanges();
                            _sessionHasRecipeRepository.addRecipeToSession(cartLine, session as Session);
                        }
                        break;
                    }
                default:
                    {
                        foreach (Session session1 in sessions)
                        {
                            if (session1.SessionName == "dinner")
                            {
                                count++;
                                _sessionHasRecipeRepository.addRecipeToSession(cartLine, session1 as Session);
                                break;
                            }
                        }
                        if (count != 0)
                        {
                            break;
                        }
                        else
                        {
                            session = new Session
                            {
                                SessionName = "dinner",
                                DayId = day.DayId
                            };
                            _dbSet.Add(session);
                            _context.SaveChanges();
                            _sessionHasRecipeRepository.addRecipeToSession(cartLine, session as Session);
                        }
                        break;
                    }
            }
        }
        public bool RemoveSession(Day day)
        {
            List<Session> sessions = _dbSet.Where(s => s.DayId == day.DayId).ToList();
            
            SessionHasRecipeRepository _sessionHasRecipeRepository = new SessionHasRecipeRepository();
            //Console.WriteLine("save S");
            List<Session> sessions1 = new List<Session>();
            foreach (Session session in sessions)
            {
                switch (session.SessionName)
                {
                    case "breakfast":
                        {
                            
                            if (_sessionHasRecipeRepository.RemoveRecipeToSession(session))
                            {
                                sessions1.Add(session);
                            }
                            break;
                        }
                    case "lunch":
                        {
                            if (_sessionHasRecipeRepository.RemoveRecipeToSession(session))
                            {
                                sessions1.Add(session);
                            }
                            break;
                        }
                    default:
                        {
                            if (_sessionHasRecipeRepository.RemoveRecipeToSession(session))
                            {
                                sessions1.Add(session);
                            }
                            break;
                        }
                }
            }
            if (sessions1.Count > 0)
            {
                foreach (Session session in sessions1)
                {
                    Delete(session);
                }
            }
            return true;
        }
        public bool SaveSession(List<CartLine> cartLines, Day day)
        {
            List<Session> sessions = _dbSet.Where(s => s.DayId == day.DayId).ToList();

            SessionHasRecipeRepository _sessionHasRecipeRepository = new SessionHasRecipeRepository();
            //Console.WriteLine("save S");
            bool flag = false;
            List<Session> sessions1 = new List<Session>();
            foreach (Session session in sessions)
            {
                switch (session.SessionName)
                {
                    case "breakfast":
                        {
                            foreach (CartLine cartLine in cartLines)
                            {
                                if (cartLine.SlotID % 3 == 1) {
                                    _sessionHasRecipeRepository.addRecipeToSession(cartLine, session);
                                    cartLines.Remove(cartLine);
                                    flag = true;
                                    break;
                                }
                            }
                            if (!flag)
                            {
                                if (_sessionHasRecipeRepository.RemoveRecipeToSession(session))
                                {
                                    sessions1.Add(session);
                                }
                            }
                            
                            break;
                        }
                    case "lunch":
                        {
                            foreach (CartLine cartLine in cartLines)
                            {
                                if (cartLine.SlotID % 3 == 2)
                                {
                                    _sessionHasRecipeRepository.addRecipeToSession(cartLine, session);
                                    cartLines.Remove(cartLine);
                                    flag = true;
                                    break;
                                }
                            }
                            if (!flag)
                            {
                                if (_sessionHasRecipeRepository.RemoveRecipeToSession(session))
                                {
                                    sessions1.Add(session);
                                }
                            }
                            break;
                        }
                    default:
                        {
                            foreach (CartLine cartLine in cartLines)
                            {
                                if (cartLine.SlotID % 3 == 0)
                                {
                                    _sessionHasRecipeRepository.addRecipeToSession(cartLine, session);
                                    cartLines.Remove(cartLine);
                                    flag = true;
                                    break;
                                }
                            }
                            if (!flag)
                            {
                                if (_sessionHasRecipeRepository.RemoveRecipeToSession(session))
                                {
                                    sessions1.Add(session);
                                }
                            }
                            break;
                        }
                }
            }
            if (cartLines.Count > 0)
            {
                foreach(CartLine cartLine in cartLines)
                {
                    addSession(cartLine, day);
                }
                
            }
            if (sessions1.Count > 0)
            {
                foreach (Session session in sessions1)
                {
                    Delete(session);
                }
            }
            return true;
        }
        public Slot showSessions(MealPlanning meal, int dayOfWeek, Day day)
        {
            SessionHasRecipeRepository _sessionHasRecipeRepository = new SessionHasRecipeRepository();
            //SessionRepository _sessionRepository = new SessionRepository();
            var sessions = getSessionByDay(day);
            Slot slot = new Slot();
            List<Session> sessions2 = new List<Session>();
            foreach (Session session in sessions)
            {
                switch (session.SessionName)
                {
                    case "breakfast":
                        {
                            if (!slot.AddSlot(_sessionHasRecipeRepository.showRecipeToSession(meal, 3 * dayOfWeek + 1, session)))
                            {
                                sessions2.Add(session);
                            }
                            break;
                        }
                    case "lunch":
                        {
                            if (!slot.AddSlot(_sessionHasRecipeRepository.showRecipeToSession(meal, 3 * dayOfWeek + 2, session)))
                            {
                                sessions2.Add(session);
                            }
                            break;
                        }
                    default:
                        {
                            if (!slot.AddSlot(_sessionHasRecipeRepository.showRecipeToSession(meal, 3 * dayOfWeek + 3, session)))
                            {
                                sessions2.Add(session);
                            }
                            break;
                        }
                }
            }
            if (sessions2.Count > 0)
            {
                foreach (Session session in sessions2)
                {
                    Delete(session);
                }
            }
            return slot;
        }
    }
}

    

        
