using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
	public class Slot : RepositoryBase<Recipe>
	{
		public List<CartLine> Lines { get; set; } = new List<CartLine>();
		public void AddItemToSlot(Recipe recipe, int slotNow)
		{
			CartLine? line = Lines
			.Where(p => p.SlotID == slotNow && p.Recipes.RecipeId > 0)
			.FirstOrDefault();

			if (line == null )
			{

				Lines.Add(new CartLine
				{
					Recipes = recipe,
					//Quantity = quantity
					SlotID = slotNow

				});
			}
			else
			{
				Console.WriteLine("da co recipe");
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

		public void RemoveSlot(int slotNow, Recipe recipe) =>
			Lines.RemoveAll(l => l.SlotID == slotNow && l.Recipes.RecipeId == recipe.RecipeId);

		

		public void Clear() => Lines.Clear();

	}

	public class CartLine
	{
		public int CartLineID { get; set; } // ko biet la gi

		public Recipe Recipes { get; set; } = new();   // mo awn

		public int SlotID { get; set; }   // vi tri 1 -21

	}
}
	
