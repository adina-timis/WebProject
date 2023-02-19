using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebProject.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
   policy.RequireRole("Admin"));
});

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Servicii");
    options.Conventions.AllowAnonymousToPage("/Servicii/Index");
    options.Conventions.AllowAnonymousToPage("/Servicii/Details");
    options.Conventions.AuthorizeFolder("/Clienti", "AdminPolicy");
    options.Conventions.AuthorizeFolder("/Marci", "AdminPolicy");
    options.Conventions.AuthorizeFolder("/Categorii", "AdminPolicy");
});
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<WebProjectContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebProjectContext") ?? throw new InvalidOperationException("Connection string 'WebProjectContext' not found.")));
builder.Services.AddDbContext<LibraryIdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebProjectContext") ?? throw new InvalidOperationException("Connectionstring 'WebProjectContext' not found.")));
//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<LibraryIdentityContext>();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>()
 .AddEntityFrameworkStores<LibraryIdentityContext>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); ;

app.UseAuthorization();

app.MapRazorPages();

app.Run();
