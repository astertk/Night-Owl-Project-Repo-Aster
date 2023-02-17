using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DWC_NightOwlProject.Data;
using DWC_NightOwlProject.DAL.Abstract;
using DWC_NightOwlProject.DAL.Concrete;
using OpenAI;
using OpenAI.Images;
using OpenAI.Models;



//calling openAI
var builder = WebApplication.CreateBuilder(args);
var APIKey = builder.Configuration["APIKey"];
var api = new OpenAIClient(new OpenAIAuthentication(APIKey));
var background = await api.CompletionsEndpoint.CreateCompletionAsync("Create my background story for my Dungeons and Dragons Campaign. Theme: Comical. Mogarr the Loser wants to steal all the music from the realm!", max_tokens: 200, temperature: 0.8, presencePenalty: 0.1, frequencyPenalty: 0.1, model: Model.Davinci);
Console.WriteLine(background);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("IdentityConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var connectionStringApp = builder.Configuration.GetConnectionString("AppConnection");
builder.Services.AddDbContext<WebAppDbContext>(options => options
                                    .UseLazyLoadingProxies()
                                    .UseSqlServer(connectionStringApp));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IMaterialRepository, MaterialRepository>();
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
