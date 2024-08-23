using System;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using ValeShop.Automapper;
using ValeShop.CountryService;
using ValeShop.Data;
using ValeShop.interfaces;
using ValeShop.Repositories;
using ValeShop.ViewModels;
using ValeShop.PaymentServices;
using ECommerce.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var jwtSettings = builder.Configuration.GetSection("Jwt");

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["AuthToken"];
                return Task.CompletedTask;
            }
        };
    });

var cloudinarySettings = builder.Configuration.GetSection("CloudinarySettings").Get<CloudinarySettings>();
var account = new Account(
    cloudinarySettings.CloudName,
    cloudinarySettings.ApiKey,
    cloudinarySettings.ApiSecret
);
var cloudinary = new Cloudinary(account);
builder.Services.AddSingleton(cloudinary);


builder.Services.AddSingleton(cloudinary);
builder.Services.AddScoped<IImageRepository, ImageServiceRepository>();

builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); 
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<ICartRepository, CartRepository>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpClient<CountryServiceClass>();
builder.Services.AddTransient<CountryServiceClass>();
builder.Services.AddScoped<CountryDataSeeder>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IStateRepository, StateRepository>();
builder.Services.AddSingleton<PaystackService>();

builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("Smtp"));

builder.Services.AddTransient<IEmailService, EmailService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<CountryDataSeeder>();
    await seeder.SeedAsync();
}

app.Run();

public class CloudinarySettings
{
    public string CloudName { get; set; }
    public string ApiKey { get; set; }
    public string ApiSecret { get; set; }
}