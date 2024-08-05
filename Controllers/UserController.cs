using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ValeShop.interfaces;
using ValeShop.Models;
using ValeShop.ViewModels;

namespace ValeShop.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserController(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        // GET: /User/Register
        public IActionResult Register()
        {
            return View(new UserViewModel());
        }

        // GET: /User/Login
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("sessionId") != null)
            {
                return RedirectToAction("GetAllProducts", "Product");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("modelstate is not valid");
                return View();
            }

            try
            {
                var user = _mapper.Map<User>(userViewModel);
                var registeredUser = await _userRepository.CreateUser(user);
                TempData["UserSuccessMessage"] = "User registration successful";
                return RedirectToAction("index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Cannot register user"+ ex);
                return View(userViewModel);
            }
        }

        /*Login Controller*/
        
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["LoginFailed"] = "incorrect email/password";
                return View(loginViewModel);
            }

            try
            {
                var user = await _userRepository.Login(loginViewModel);
                if (user == null)
                {
                    TempData["LoginFailed"] = "incorrect email/password";
                    return View(loginViewModel);
                }
                HttpContext.Session.SetString("sessionId", Guid.NewGuid().ToString());
                HttpContext.Session.SetString("userId", user.Id.ToString());
                /*var token = GenerateJwtToken(user);
                var cookieOptions = new CookieOptions()
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddMinutes(30)
                };
                Response.Cookies.Append("AuthToken", token, cookieOptions);*/

                TempData["Success"] = "Login successful";
                return RedirectToAction("index", "Home");
            }
            catch (Exception e)
            {
                TempData["LoginFailed"] = "incorrect email/password: " + e.Message;

                return View(loginViewModel);
            }
        }


        [HttpGet]
        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("sessionId") != null)
            {
                HttpContext.Session.Clear();
            }

            return RedirectToAction("Login", "User");
            
        }



        //Authenticate User

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}