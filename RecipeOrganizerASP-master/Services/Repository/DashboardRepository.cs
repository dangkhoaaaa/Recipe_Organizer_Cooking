﻿using Microsoft.EntityFrameworkCore;
using Services.Models;
using Services.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class DashboardRepository : RepositoryBase<Recipe>
    {

        Recipe_OrganizerContext _context;
        RecipeRepository _recipeRepository;
        UserRepository _userRepository;
        FeedbackRepository _feedbackRepository;
        CategoryRepository _categoryRepository;
        public DbSet<Recipe> _dbSet;
        protected DbSet<AppUser> _dbSetUser;

        public DashboardRepository()
        {
            _context = new Recipe_OrganizerContext();
            _dbSet = _context.Set<Recipe>();
            _recipeRepository = new RecipeRepository();
            _userRepository = new UserRepository();
            _feedbackRepository = new FeedbackRepository();
            _categoryRepository = new CategoryRepository();
            _dbSetUser = _context.Set<AppUser>();
        }
        public int GetRecipebyPending()
        {
            int count = 0;
            List<Recipe> listRecipe = _recipeRepository.GetAll();
            List<Recipe> pendingRecipes = new List<Recipe>();
            if (listRecipe != null)
            {
                foreach (var item in listRecipe)
                {

                    if (item.Status.Equals("pending"))
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public int GetRecipebyReject()
        {
            int count = 0;
            List<Recipe> listRecipe = _recipeRepository.GetAll();
            List<Recipe> pendingRecipes = new List<Recipe>();
            if (listRecipe != null)
            {
                foreach (var item in listRecipe)
                {
                    if (item.Status.Equals("rejected"))
                    {
                        count++;
                    }
                }
            }
            return count;
        }



        public int GetRecipebyPublic()
        {
            int count = 0;
            List<Recipe> listRecipe = _recipeRepository.GetAll();
            List<Recipe> pendingRecipes = new List<Recipe>();
            if (listRecipe != null)
            {
                foreach (var item in listRecipe)
                {
                    if (item.Status.Equals("public"))
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public int GetRecipebyDraft()
        {
            int count = 0;
            List<Recipe> listRecipe = _recipeRepository.GetAll();
            List<Recipe> pendingRecipes = new List<Recipe>();
            if (listRecipe != null)
            {
                foreach (var item in listRecipe)
                {
                    if (item.Status.Equals("draft"))
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public int GetRecipebyTrash()
        {
            int count = 0;
            List<Recipe> listRecipe = _recipeRepository.GetAll();
            List<Recipe> pendingRecipes = new List<Recipe>();
            if (listRecipe != null)
            {
                foreach (var item in listRecipe)
                {
                    if (item.Status.Equals("trash"))
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public int TotalAccount()
        {
            int count = 0;
            count = _userRepository.GetAll().Count();
            return count;
        }
        public int TotalRecipe()
        {
            int count = 0;
            count = _recipeRepository.GetAll().Count();
            return count;
        }

        public int TotalFeedback()
        {
            int count = 0;
            count = _feedbackRepository.GetAll().Count();
            return count;
        }

        public int TotalCategiry()
        {
            int count = 0;
            count = _categoryRepository.GetAll().Count();
            return count;
        }
        public int TotalView()
        {
            int count = 0;
            List<Recipe> listRecipe = _recipeRepository.GetAll();
            List<Recipe> pendingRecipes = new List<Recipe>();
            if (listRecipe != null)
            {
                foreach (var item in listRecipe)
                {
                    count += item.NumberShare;
                }
            }
            return count;
        }

        public int Top1Category()
        {
            int count = 0;
            List<Recipe> listRecipe = _recipeRepository.GetAll();
            List<Recipe> pendingRecipes = new List<Recipe>();
            if (listRecipe != null)
            {
                foreach (var item in listRecipe)
                {
                    count += item.NumberShare;
                }
            }
            return count;
        }

        public List<Recipe> Top5Recipe()
        {
            int count = 0;
            List<Recipe> listRecipe = _recipeRepository.GetAll();
            listRecipe = listRecipe.OrderByDescending(r => r.NumberShare).Take(5).ToList();

            return listRecipe;
        }

        public int NumberRecipeTime(int month, int year)
        {
            List<Recipe> listRecipe = _recipeRepository.GetAll();
            listRecipe = listRecipe.Where(r => r.Date.Year == year && r.Date.Month == month).ToList();

            int num = listRecipe.Count();
            return num;
        }

        public int NumberOldUser(int start, int end)
        {
            DateTime endDate = DateTime.Today.AddYears(-start);
            DateTime startDate = DateTime.Today.AddYears(-(end + 1));

            List<AppUser> listUser = _dbSetUser.Where(u => u.Birthday.HasValue &&
                                                           u.Birthday.Value >= startDate &&
                                                           u.Birthday.Value <= endDate)
                                               .ToList();

            int num = listUser.Count();
            return num;
        }

    }
}
