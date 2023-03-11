using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DWC_NightOwlProject.Data;
using DWC_NightOwlProject.DAL.Abstract;
using DWC_NightOwlProject.DAL.Concrete;
using Microsoft.Extensions.DependencyInjection;
using OpenAI.GPT3.Extensions;
using OpenAI.GPT3.ObjectModels.RequestModels;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.Managers;
using Microsoft.Build.Framework;
using OpenAI.Edits;
using OpenAI;
using OpenAI.Images;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenAIService(settings => { settings.ApiKey = Environment.GetEnvironmentVariable("APIKey"); });

//var api = new OpenAIClient(new OpenAIAuthentication("")); // insert your APIKey 

//var mapList = await api.ImagesEndPoint.GenerateImageAsync("Draw an icon for the word \"Backstory\". It should be a little book with its pages open, facing outward. Make it a black-and-white image. Very simple, and clean, to be used on a website. Make it a png that I can screenshot.", 1, ImageSize.Large);




// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("IdentityConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var connectionStringApp = builder.Configuration.GetConnectionString("AppConnection");
builder.Services.AddDbContext<WebAppDbContext>(options => options
                                    .UseLazyLoadingProxies()
                                    .UseSqlServer(connectionStringApp));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IMaterialRepository, MaterialRepository>();
builder.Services.AddScoped<IWorldRepository, WorldRepository>();
builder.Services.AddScoped<DbContext, WebAppDbContext>();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
