using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecipeOrganizer.Data;
using Services.Models.Authentication;
using Services.Repository;

namespace RecipeOrganizer.Controllers
{
	public class SlotController : Controller
	{

		private readonly RecipeRepository _recipeRepository;
		private readonly IngredientRepository _ingredientRepository;
		private readonly DirectionRepository _directionRepository;
		private readonly RecipeHasTagRepository _recipeHasTagRepository;
		private readonly TagRepository _tagRepository;
		private readonly RecipeHasCategoryRepository _recipeHasCategoryRepository;
		private readonly MetadataRepository _metadataRepository;
		private readonly MediaRepository _mediaRepository;
		private readonly UserManager<AppUser> _userManager;
		private readonly Slot _slot;

		public SlotController(UserManager<AppUser> userManager)
		{
			_recipeRepository = new RecipeRepository();
			_ingredientRepository = new IngredientRepository();
			_directionRepository = new DirectionRepository();
			_recipeHasTagRepository = new RecipeHasTagRepository();
			_tagRepository = new TagRepository();
			_recipeHasCategoryRepository = new RecipeHasCategoryRepository();
			_metadataRepository = new MetadataRepository();
			_mediaRepository = new MediaRepository();
			_slot = new Slot();
			_userManager = userManager;
		}
		public Slot? slot { get; set; }

		private readonly ApplicationDbContext _context;

		public SlotController(ApplicationDbContext context)
		{
			_context = context;
		}
		//public IActionResult Index()
		//{
		//	return View("Cart", HttpContext.Session.GetJson<Slot>("cart"));
		//}
		//public IActionResult AddToCart(int productId)
		//{
		//	Product? product = _context.Products
		//	.FirstOrDefault(p => p.ProductId == productId);
		//	if (product != null)
		//	{
		//		Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
		//		Cart.AddItem(product, 1);
		//		HttpContext.Session.SetJson("cart", Cart);
		//	}
		//	return View("Cart", Cart);
		//}

		//public IActionResult UpdateToCart(int productId)
		//{
		//	Product? product = _context.Products
		//	.FirstOrDefault(p => p.ProductId == productId);
		//	if (product != null)
		//	{
		//		Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
		//		Cart.AddItem(product, -1);
		//		HttpContext.Session.SetJson("cart", Cart);
		//	}
		//	return View("Cart", Cart);
		//}


		//public IActionResult RemoveFromCart(int productId)
		//{
		//	Product? product = _context.Products
		//	.FirstOrDefault(p => p.ProductId == productId);
		//	if (product != null)
		//	{
		//		Cart = HttpContext.Session.GetJson<Cart>("cart");
		//		Cart.RemoveLine(product);
		//		HttpContext.Session.SetJson("cart", Cart);
		//	}
		//	return View("Cart", Cart);
		//} 
	}
}
