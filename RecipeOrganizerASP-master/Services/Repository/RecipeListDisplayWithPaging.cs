using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class RecipeListDisplayWithPaging
    {
     
        public List<Recipe> Recipes { get; set; } = new List<Recipe>();
        public PagingInfo PagingInfo { get; set; } = new PagingInfo();
    }
}
