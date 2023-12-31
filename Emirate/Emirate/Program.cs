using Emirate.DATABASE;
using Emirate.Helper;
using Emirate.IHelper;
using Emirate.Models;
using Emirate.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserHelper, UserHelper>();
builder.Services.AddScoped<IDropdownHelper, DropdownHelper>();
builder.Services.AddScoped<IAdminHelper, AdminHelper>();
builder.Services.AddScoped<IDescriptionHelper, DescriptionHelper>();
builder.Services.AddScoped<IEmailHelper, EmailHelper>();
builder.Services.AddScoped<IEmailSettings, EmailSettings>();
builder.Services.AddScoped<IEmailServices, EmailServices>();
//builder.Services.AddScoped<IWebHostEnvironment>();
//builder.Services.AddScoped<IHostingEnvironment, HostingEnvironment>();

// Add services to the container.

//builder.services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));

builder.Services.AddSingleton<IEmailSettings>(builder.Configuration.GetSection("EmailSettings").Get<EmailSettings>());


//builder.Services.AddSingleton<IWebHostEnvironment>(env => new WebHostEnvironment { EnvironmentName = env.HostingEnvironment.EnvironmentName });
builder.Services.AddMvc();

builder.Services.AddDbContext<ApplicationDb>(opt => opt.UseSqlServer(
	builder.Configuration.GetConnectionString("EmirateTB")));

builder.Services.AddControllersWithViews();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
	options.Password.RequireDigit = false;
	options.Password.RequiredLength = 5;
	options.Password.RequiredUniqueChars = 0;
	options.Password.RequireLowercase = false;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireUppercase = false;

}).AddEntityFrameworkStores<ApplicationDb>();



/*.AddScoped<IEmailHelper, EmailHelper>()
.AddScoped<IAdminHelper, AdminHelper>();*/
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Emirate}/{action=Index}/{id?}");

app.Run();
