using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ValeShop.Data;
using ValeShop.interfaces;
using ValeShop.Models;
using ValeShop.ViewModels;

namespace ValeShop.Controllers
{
    public class BillingDetailsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ICountryRepository _countryRepository;
        private readonly IStateRepository _stateRepository;
        private readonly IEmailService _emailService;


        public BillingDetailsController(AppDbContext context, ICountryRepository countryRepository, IStateRepository stateRepository, IEmailService emailService)
        {
            _context = context;
            _countryRepository = countryRepository;
            _stateRepository = stateRepository;
            _emailService = emailService;

        }

        public async Task<IActionResult> BillingDetails(Guid? selectedCountryId)
        {
            var session = HttpContext.Session.GetString("userId");
            if (session == null)
            {
                return RedirectToAction("Login", "User");
            }
            var model = new BillingDetailsViewModel();


            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> SaveBillingDetails(BillingDetailsViewModel model)
        {
            if (!ModelState.IsValid)
            {

                return View("BillingDetails");
            }

            var billingDetails = new BillingDetails
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse(HttpContext.Session.GetString("userId")),
                CompanyName = model.CompanyName,
                Phone = model.Phone,
                Address = model.Address,
                City = model.City,
                Country = model.Country,
                State = model.State,
                // StateId = model.StateId,
                IsActive = true
            };

            await _context.BillingDetails.AddAsync(billingDetails);
            await _context.SaveChangesAsync();
            var sessionId = HttpContext.Session.GetString("sessionId");
            var cartItems = _context.Carts
                .Include(c => c.Product)
                .Where(c => c.SessionId == sessionId)
                .Select(c => new CartViewModel()
                {
                    Name = c.Product.Name ?? "",
                    ImageUrl = c.Product.ImageUrl ?? "",
                    Quantity = c.Quantity,
                    ProductId = c.ProductId,
                    Price = c.Product.Price,
                    Total = c.Product.Price * c.Quantity
                }).ToList();

            var cartItemViewModel = new CartItemViewModel
            {
                CartItems = cartItems

            };

            var userId = HttpContext.Session.GetString("userId");
            var sessionIda = HttpContext.Session.GetString("sessionId");
            var orders = new ValeShop.Models.Order
            {
                UserId = Guid.Parse(userId),
                AmountPaid = cartItemViewModel.Total,
                PaymentDateTime = DateTime.Now,
                Status = 0,
                CreatedDate = DateTime.Now
            };
            await _context.Orders.AddAsync(orders);

            var cartItem = _context.Carts.Where(c => c.SessionId == sessionIda).Include(x => x.Product).ToList();
            foreach(var item in cartItem){
                var details = new OrderDetails{
                    Quantity = item.Quantity,
                    Price = item.Product.Price * item.Quantity,
                    UnitPrice = item.Product.Price,
                    ProductId = item.ProductId,
                    OrderId = orders.Id
                };
                _context.OrderDetails.Add(details);
            }
            await _context.SaveChangesAsync();

            //after saving orders, we then retrieve the customer's order details

            var orderDetails = await _context.OrderDetails
            .Where(od => od.OrderId == orders.Id)
            .Include(od => od.Product)
            .ToListAsync();


            string orderDetailsHtml = FormatOrderDetailsForEmail(orders, orderDetails);



            TempData["BillingDetailsSuccess"] = "Order placed successfully";
            var user = _context.Users.FirstOrDefault(x => x.Id == Guid.Parse(userId));


            string subject = "Order Confirmation - Order #" + orders.Id;
            string body = $"Dear {user?.FirstName},<br><br>You have successfully placed an order. Below are your order details:<br><br>{orderDetailsHtml}<br><br>Sincerely,<br>Your Company";
            await _emailService.SendEmailAsync(user.Email, subject, body);

            return RedirectToAction("OrderSuccess", "Order");



        }

        private string FormatOrderDetailsForEmail(Order order, List<OrderDetails> orderDetails)
        { 

            var sb = new StringBuilder();

            sb.Append("<h2>Order Details</h2>");
            sb.Append($"<p>Order Number: {order.Id}</p>");
            sb.Append($"<p>Order Date: {order.PaymentDateTime.ToString("MM/dd/yyyy")}</p>");
            sb.Append("<table border='1' cellpadding='5' cellspacing='0'>");
            sb.Append("<tr><th>Product Image</th><th>Product Name</th><th>Quantity</th><th>Unit Price</th><th>Total Price</th></tr>");
            decimal totals = 0;
            foreach (var item in orderDetails)
            {
                Console.WriteLine($"Product: {item.Product?.Name}, ImageUrl: {item.Product?.ImageUrl}, Price: {item.Price}, Quantity: {item.Quantity}");

                sb.Append("<tr>");

                sb.Append($"<td><img src='{item.Product?.ImageUrl}' alt='{item.Product?.Name}' style='width:100px; height:auto;' /></td>");

                sb.Append($"<td>{item.Product?.Name}</td>");

                sb.Append($"<td>{item.Quantity}</td>");

                sb.Append($"<td>{item.UnitPrice:C}</td>");

                sb.Append($"<td>{item.Price:C}</td>");

                sb.Append("</tr>");
                totals += item.Price;
            }

            sb.Append("</table>");
            sb.Append($"<p>Total Amount Paid: {totals:C}</p>");

            return sb.ToString();
        }

    }
}