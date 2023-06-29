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

			if (line == null )
			{

				Lines.Add(new CartLine
				{
					Recipes = new List<Recipe> { recipe},
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
				if (count == 0) {
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
				if (line.SlotID == slotNow && line.UserID == userID && line.Week == week) {
					foreach(var item in line.Recipes)
					{
						if (item.RecipeId == recipe.RecipeId) {
						line.Recipes.Remove(item);
						break;}
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
	
