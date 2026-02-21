using GameStore.Data.Identity;
using GameStore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GAMEstore23.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(ICartService cartService, IOrderService orderService, UserManager<ApplicationUser> userManager)
        {
            _cartService = cartService;
            _orderService = orderService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var cartItems = _cartService.GetCartItems(userId);
            ViewBag.Total = _cartService.GetTotal(userId);
            return View(cartItems);
        }

        [HttpPost]
        public IActionResult AddToCart(int gameId)
        {
            var userId = _userManager.GetUserId(User);
            _cartService.AddToCart(gameId, userId);
            return RedirectToAction("Index", "Games");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int gameId)
        {
            var userId = _userManager.GetUserId(User);
            _cartService.RemoveFromCart(gameId, userId);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Checkout()
        {
            var userId = _userManager.GetUserId(User);
            var cartItems = _cartService.GetCartItems(userId);
            if (cartItems.Count == 0)
                return RedirectToAction("Index", "Games");

            var order = await _orderService.CreateOrderAsync(userId, cartItems);
            _cartService.ClearCart(userId);
            return RedirectToAction("Details", "Orders", new { id = order.Id });
        }
    }
}