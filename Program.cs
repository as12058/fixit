using Fixit.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Google;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//----------------Configure Authentication Services----------------//


// Add services to the container.
builder.Services.AddControllersWithViews();

// --- Your existing DbContext configuration (Example) ---
builder.Services.AddDbContext<FixitDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// --- End existing DbContext configuration ---


// --- CRUCIAL SECTION: Configure Authentication Services ---
builder.Services.AddAuthentication(options =>
{
    // You likely already have this for your default cookie scheme
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme; // Set Google as a default challenge scheme
})
    .AddCookie(options =>
    {
        // Set the path to your login page. When an unauthorized user tries to access a protected resource,
        // they will be redirected to this path.
        options.LoginPath = "/Users/Login";

        // Set the path for handling logout. This is where HttpContext.SignOutAsync() will redirect after signing out.
        options.LogoutPath = "/Home";

        // Optional: Path for when a user is authenticated but not authorized (e.g., lacks a specific role).
        // options.AccessDeniedPath = "/Home/AccessDenied";

        // Set the default expiration time for the authentication cookie.
        // If "Remember Me" is not checked, the cookie typically expires when the browser session ends
        // or after this short duration.
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);

        // If true, the cookie expiration will be reset if the user is active within a certain period.
        // This extends the user's session without forcing them to log in again, as long as they keep using the site.
        options.SlidingExpiration = true;

        // You can add more cookie settings here, like Name, Domain, SecurePolicy etc.
    })
    .AddGoogle(googleOptions => // ADD THIS BLOCK
    {
        // Retrieve ClientId and ClientSecret from configuration (User Secrets or appsettings.json)
        googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"] ?? throw new InvalidOperationException("Google ClientId not found.");
        googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"] ?? throw new InvalidOperationException("Google ClientSecret not found.");

        // Optional: Add scopes to request more user information
        // googleOptions.Scope.Add("profile"); // To get first name, last name etc.
        // googleOptions.Scope.Add("email");   // To get email address (already included by default often)
    });

// --- END OF Configure Authentication Services SECTION ---//




var app = builder.Build(); // This line creates the application instance


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

//------------Configure Authentication Middleware ---------------//
// Authentication MUST come BEFORE Authorization
app.UseAuthentication(); // This middleware reads the authentication cookie
                         // and populates HttpContext.User with the user's identity (claims).
app.UseAuthorization();  // This middleware checks if the user has permission to access
                         // a resource based on roles or policies (e.g., [Authorize] attributes).
// --- END OF CRUCIAL SECTION ---





app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Users}/{action=Signup}");



app.Run();
