using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class Slot : RepositoryBase<Slot>
    {
        public List<CartLine> Lines { get; set; } = new List<CartLine>();
        public void AddItemToSlot(Recipe recipe, int slotNow, string userID, string week)
        {
            CartLine? line = Lines
            .Where(p => p.SlotID == slotNow)
            .FirstOrDefault();

            if (line == null)
            {

                Lines.Add(new CartLine
                {
                    Recipes = new List<Recipe> { recipe },
                    //Quantity = quantity
                    UserID = userID,
                    Week = week,
                    SlotID = slotNow

                });
            }
            else
            {

                int count = 0;
                foreach (var item in line.Recipes)
                {
                    if (item.RecipeId == recipe.RecipeId)
                    {
                        count++;
                        break;
                    }
                }
                if (count == 0)
                {
                    line.Recipes.Add(recipe);
                }
            }
        }

        //public void UpdateItemToSlot(Recipe recipe, int slotNow)
        //{
        //	//tim cartline muon update
        //	CartLine? line = Lines
        //	.Where(p => p.SlotID == slotNow)
        //	.FirstOrDefault();

        //	if (line == null)
        //	{
        //		//bao loi
        //	}
        //	else
        //	{
        //		Lines.Add(new CartLine
        //		{
        //			Recipes = recipe,
        //			//Quantity = quantity
        //			SlotID = slotNow

        //		});
        //	}
        //}

        public void RemoveSlot(int slotNow, string userID, string week) =>
            Lines.RemoveAll(l => l.SlotID == slotNow
            && l.Week == week && l.UserID == userID);

        public void RemoveRecipe(int slotNow, Recipe recipe, string userID, string week)
        {
            foreach (var line in Lines)
            {
                if (line.SlotID == slotNow && line.UserID == userID && line.Week == week)
                {
                    foreach (var item in line.Recipes)
                    {
                        if (item.RecipeId == recipe.RecipeId)
                        {
                            line.Recipes.Remove(item);
                            break;
                        }
                    }
                    if (line.Recipes.Count == 0)
                    {
                        RemoveSlot(slotNow, userID, week);
                    }
                    break;
                }
            }

        }



        public void Clear() => Lines.Clear();
        public Boolean AddSlot(Slot slot)
        {
            if (slot != null)
            {
                foreach (var line in slot.Lines)
                {
                    Lines.Add(line);
                }
                return true;
            }
            return false;

        }

        //function random
        public List<int> GetRandomIds(List<int> idList, int num)
        {
            List<int> randomIds = new List<int>();

            if (idList.Count <= num)
            {
                randomIds.AddRange(idList);
            }
            else
            {
                ShuffleIds(idList);

                randomIds.AddRange(idList.GetRange(0, num));
            }

            return randomIds;
        }
        public void ShuffleIds(List<int> idList)
        {
            Random random = new Random();
            int n = idList.Count;

            for (int i = n - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                int temp = idList[i];
                idList[i] = idList[j];
                idList[j] = temp;
            }
        }

        public Slot SuggestRecipes (string week, string userId)
        {
            Slot slot = new Slot();
            if (week != null)
            {
                
                RecipeRepository _repository = new RecipeRepository();
                CategoryRepository _categoryRepository = new CategoryRepository();
                RecipeHasCategoryRepository _recipeHasCategoryRepository = new RecipeHasCategoryRepository();
                List<Category> categoryList = _categoryRepository.GetAll();
                List<int> listIdCate = new List<int>();
                foreach (Category category in categoryList)
                {
                    listIdCate.Add(category.CategoryId);
                }
                var listAfterRandom = GetRandomIds(listIdCate, 3);
                List<int> listIdRecipe = new List<int>();
                foreach (int id in listAfterRandom)
                {
                    List<int> listIDRecipeOfCate = new List<int>();
                    var listRecipes = _recipeHasCategoryRepository.getRecipeByCategoryID(id);
                    foreach (var recipe in listRecipes)
                    {
                        if (_repository.GetById(recipe.RecipeId, "public") != null)
                        {
                            listIDRecipeOfCate.Add(recipe.RecipeId);
                        }
                        
                    }
                    foreach (var recipe in GetRandomIds(listIDRecipeOfCate, 7))
                    {
                        listIdRecipe.Add(recipe);
                    }
                }
                if(listIdRecipe.Count < 21)
                {
                    
                    foreach (var recipe in AddMoreRecipesToList(21 - listIdRecipe.Count()))
                    {
                        listIdRecipe.Add(recipe);
                    }
                }
                GetRandomIds(listIdRecipe, listIdRecipe.Count);
                slot.Lines = new List<CartLine>();
                for (int i = 0; i < listIdRecipe.Count; i++)
                {

                    slot.Lines.Add(new CartLine
                    {
                        SlotID = i + 1,
                        Recipes = new List<Recipe>() { _repository.GetById(listIdRecipe[i], "public") },
                        Week = week,
                        UserID = userId
                    });

                }
            }
            
            return slot;
        }

        //add more if list not enough to 21 recipes

        public List<int> AddMoreRecipesToList (int num)
        {
            List<int> list = new List<int>();   
            RecipeRepository _repository = new RecipeRepository();
            CategoryRepository _categoryRepository = new CategoryRepository();
            RecipeHasCategoryRepository _recipeHasCategoryRepository = new RecipeHasCategoryRepository();
            var listRecipes = _recipeHasCategoryRepository.GetAll().Where(l => _repository.GetById(l.RecipeId, "public") != null).ToList();
            List<int> listIDRecipeOfCate = new List<int>();
            foreach (var recipe in listRecipes)
            {
                listIDRecipeOfCate.Add(recipe.RecipeId);
            }
            foreach (var recipe in GetRandomIds(listIDRecipeOfCate, num))
            {
                list.Add(recipe);
            }
            return list;
        }
        public Slot SuggestRecipesHasVegetarianDay (string week, string userId, string day)
        {
            RecipeRepository _repository = new RecipeRepository();
            CategoryRepository _categoryRepository = new CategoryRepository();
            RecipeHasCategoryRepository _recipeHasCategoryRepository = new RecipeHasCategoryRepository();
            Slot slot = new Slot();
            List<int> listVegatarian = new List<int>();
            List<int> selectedDays = new List<int>();
            if (day.Equals("all"))
            {
                
                var listRecipes = _recipeHasCategoryRepository.GetAll().Where(l => _repository.GetById(l.RecipeId, "public" ) != null && l.CategoryId.Equals(5)).ToList();
                foreach (var recipe in listRecipes)
                {
                    listVegatarian.Add((int)recipe.RecipeId);
                }
                GetRandomIds(listVegatarian, 21);
            }
            else
            {
                selectedDays = day.Split(',').Select(days => int.Parse(days)).ToList();
                var listRecipes = _recipeHasCategoryRepository.GetAll().Where(l => _repository.GetById(l.RecipeId, "public") != null && l.CategoryId.Equals(5)).ToList();
                foreach (var recipe in listRecipes)
                {
                    listVegatarian.Add((int)recipe.RecipeId);
                }
                GetRandomIds(listVegatarian, 3 * selectedDays.Count());
            }
            List<int> addMoreRecipes = new List<int>();

            if (listVegatarian.Count < 21)
            {
               addMoreRecipes = AddMoreRecipesToList(21 -  listVegatarian.Count);
                GetRandomIds(addMoreRecipes, 21 - listVegatarian.Count());
            }
             
            slot.Lines = new List<CartLine>();
            int j = 0; 
            int z = 0;
            int w = 0;
            for (int i = 0; i < 21; i++)
            {
                if (day.Equals("all"))
                {
                    if (z < listVegatarian.Count())
                    {
                        if (i % 3 == 2)
                        {
                            z = i + 1;
                        }
                        if  (i < listVegatarian.Count())
                        {
                            slot.Lines.Add(new CartLine
                            {
                                SlotID = i + 1,
                                Recipes = new List<Recipe>() { _repository.GetById(listVegatarian[i], "public") },
                                Week = week,
                                UserID = userId
                            });
                        }
                        
                    } else
                    {
                        slot.Lines.Add(new CartLine
                        {
                            SlotID = i + 1,
                            Recipes = new List<Recipe>() { _repository.GetById(addMoreRecipes[i - listVegatarian.Count()], "public") },
                            Week = week,
                            UserID = userId
                        });
                    }
                    
                }
                else
                {
                    if (selectedDays.Count() > j && selectedDays[j].Equals(i + 1))
                    {
                        if (listVegatarian.Count() > z)
                        {
                            slot.Lines.Add(new CartLine
                            {
                                SlotID = i + 1,
                                Recipes = new List<Recipe>() { _repository.GetById(listVegatarian[z], "public") },
                                Week = week,
                                UserID = userId
                            });
                            
                        }
                        selectedDays[j]++;
                        z++;
                        if (selectedDays[j] % 3 == 1)
                        {
                            j++;
                        }
                    }
                    else
                    {
                        slot.Lines.Add(new CartLine
                        {
                            SlotID = i + 1,
                            Recipes = new List<Recipe>() { _repository.GetById(addMoreRecipes[w], "public") },
                            Week = week,
                            UserID = userId
                        });
                        w++;
                    }
                }
                

            }

            return slot;
        }
    }


    public class CartLine
    {
        public int CartLineID { get; set; } // ko biet la gi

        public string Week { get; set; }

        public string UserID { get; set; }

        public List<Recipe> Recipes { get; set; } = new();   // mo awn

        public int SlotID { get; set; }   // vi tri 1 -21

    }


}

