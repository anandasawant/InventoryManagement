using Microsoft.AspNetCore.Mvc;
using MiniInventoryManagementSystem.Contexts;
using MiniInventoryManagementSystem.Models;


namespace MiniInventoryManagementSystem.Controllers
{
    public class OrdersController : Controller
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult PlaceOrder()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder(Order order)
        {
            var product = await _context.Products.FindAsync(order.ProductId);

            if (product == null || order.Quantity > product.Stock)
            {
                ViewBag.Message = "Insufficient stock or invalid product.";
                return View(order);
            }

            product.Stock -= order.Quantity;
            order.TotalPrice = order.Quantity * product.Price;

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            ViewBag.Message = "Order placed successfully!";
            return View();
        }
    }
}
