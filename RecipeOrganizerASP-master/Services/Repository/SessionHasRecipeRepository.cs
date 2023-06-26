using Microsoft.EntityFrameworkCore;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class SessionHasRecipeRepository : RepositoryBase<SessionHasRecipe>
    {
        Recipe_OrganizerContext _context;

        DbSet<SessionHasRecipe> _dbSet;

        public SessionHasRecipeRepository()
        {
            _context = new Recipe_OrganizerContext();
            _dbSet = _context.Set<SessionHasRecipe>();
        }

        public List<SessionHasRecipe> getSessionHasRecipeBySession(Session session)
        {
            var record = _dbSet.Where(sr => sr.SessionId == session.SessionId).ToList();
            return record;
        }


        public Slot showRecipeToSession(MealPlanning meal, int slotID, Session session)
        {
            RecipeRepository _recipeRepository = new RecipeRepository();
            var recipes = getSessionHasRecipeBySession(session);
            Slot slot = new Slot();
            if (recipes.Count == 0)
            {
                return slot;
            }
            else
            {
                List<Recipe> recipes1 = new List<Recipe>();
                foreach (var recipe in recipes)
                {
                    recipes1.Add(_recipeRepository.GetById(recipe.RecipeId));
                }

                    CartLine line = new CartLine
                    {
                        SlotID = slotID,
                        Week = meal.WeekStartDate,
                        UserID = meal.UserId,
                        Recipes = recipes1
                    };
                    slot.Lines.Add(line);
                //}
            }
            return slot;
        }
        public bool RemoveRecipeToSession(Session session)
        {
            var sessionHasRecipe = _dbSet.Where(sr => sr.SessionId == session.SessionId).ToList();
            
            if (sessionHasRecipe != null)
            {
                foreach(var recipe in sessionHasRecipe)
                {
                    Delete(recipe);
                }
            }
            return true;
        }
        public void addRecipeToSession(CartLine cartLine, Session session)
        {
            var sessionHasRecipe = _dbSet.Where(sr => sr.SessionId == session.SessionId).ToList();
            Console.WriteLine("save Session");
            if (sessionHasRecipe.Count != 0)
            {
                foreach ( var recipe in sessionHasRecipe)
                {
                    Delete(recipe);
                }
            }
            else
            {
                
                foreach (var recipe in sessionHasRecipe)
                {
                    int count = 0;
                    foreach ( var recipe2 in cartLine.Recipes)
                    {
                        if (recipe.RecipeId != recipe2.RecipeId)
                        {
                            
                        }
                        else
                        {
                            count++;
                            if (count > 1)
                            {
                                Delete(recipe);
                            }
                            else if (count == 1){
                                cartLine.Recipes.Remove(recipe2 );
                            }
                        }
                    }
                    if (count == 0)
                    {
                        Delete(recipe);
                    }
                }
                SessionHasRecipe sessionHasRecipe1 = null;
                if (cartLine.Recipes.Count != 0)
                {
                    foreach (var recipe in cartLine.Recipes)
                    {
                        _dbSet.Add(sessionHasRecipe1 = new SessionHasRecipe
                        {
                            RecipeId = recipe.RecipeId,
                            SessionId = session.SessionId
                        });
                        _context.SaveChanges();
                    }
                }
                

                //_dbSet.Add(sessionHasRecipe = new SessionHasRecipe
                //{
                //    RecipeId = cartLine.Recipes.RecipeId,
                //    SessionId = session.SessionId
                //});
                //_context.SaveChanges();
            }
        }
    }
}
