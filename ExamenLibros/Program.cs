using ExamenLibros.Data;
using ExamenLibros.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
    CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme =
    CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme =
    CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie();

builder.Services.AddControllersWithViews
    (options => options.EnableEndpointRouting = false)
    .AddSessionStateTempDataProvider();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

string connectionString = builder.Configuration.GetConnectionString("SqlLibros");
builder.Services.AddTransient<RepositoryLibros>();
builder.Services.AddDbContext<LibrosContext>
    (options => options.UseSqlServer(connectionString));

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

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.UseMvc(routes =>
{
    routes.MapRoute(
        name: "default",
        template: "{controller=Managed}/{action=Login}/{id?}"
    );
});


app.Run();
