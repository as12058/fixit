
using Fixit.Models;
using Fixit.ViewModels; // Add this line
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace Fixit.Controllers
{
    public class UsersController : Controller
    {
        private readonly FixitDbContext _context;

        public UsersController(FixitDbContext context)
        {
            _context = context;
        }

        // Removed Index, Details, Edit, Delete for brevity, assume they remain if needed elsewhere.
        // However, for a *signup* page, these are typically not directly exposed for user management.

        // GET: Users/Signup (or just /Users/Create if you prefer)
        public IActionResult Signup() // Renamed Create to Signup for clarity
        {
            // Populate the City dropdown.
            // You might want to select "Name" instead of "Id" for display in the dropdown.
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name");
            return View(); // This will look for Views/Users/Signup.cshtml
        }

        // POST: Users/Signup
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signup(UserSignupViewModel model) // Changed User model to UserSignupViewModel
        {
            if (ModelState.IsValid)
            {
                // Map data from ViewModel to your User model
                var user = new User
                {
                    UserName = model.UserName,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    // Hash the password before saving!
                    // This is a crucial security step. You'll need a password hashing library (e.g., BCrypt.Net-Core)
                    Password = model.Password, // Placeholder, implement hashing here!
                    PhoneNumber = model.PhoneNumber,
                    CityId = model.CityId,
                    Address = model.Address,
                    UserType = "Customer", // Set a default user type for signup
                    CreatedAt = DateTime.UtcNow // Set creation date
                };

                _context.Add(user);
                await _context.SaveChangesAsync();

                // Redirect to a confirmation page or login page
                return RedirectToAction("RegistrationSuccess"); // Create a new action for this
            }

            // If validation fails, repopulate the City dropdown and return the view with errors
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", model.CityId);
            return View(model);
        }

        // A simple success page
        public IActionResult RegistrationSuccess()
        {
            return View();
        }

        // GET: Users/Login
        public IActionResult Login()
        {
            return View(); // This will look for Views/Users/Login.cshtml
        }

        // POST: Users/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            // Store returnUrl for redirection after successful login
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                // 1. Find the user by email
                var user = await _context.Users
                                         .FirstOrDefaultAsync(u => u.Email == model.Email);

                if (user != null)
                {
                    // 2. !!! IMPORTANT: VERIFY THE HASHED PASSWORD HERE !!!
                    // In a real application, you would hash model.Password and compare it
                    // with user.Password (which should also be a hash).
                    // For example, if using BCrypt.Net-Core:
                    // bool isPasswordValid = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);

                    bool isPasswordValid = (model.Password == user.Password); // DANGER: Placeholder for hashed password verification!

                    if (isPasswordValid)
                    {
                        // 3. Create claims for the authenticated user
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                            new Claim(ClaimTypes.Name, user.UserName),
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.Role, user.UserType) // Add the user's role (UserType)
                            // Add any other claims you need, e.g., FirstName, LastName
                        };

                        var claimsIdentity = new ClaimsIdentity(
                            claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        var authProperties = new AuthenticationProperties
                        {
                            IsPersistent = model.RememberMe, // Persist cookie across browser sessions
                            ExpiresUtc = model.RememberMe ? DateTimeOffset.UtcNow.AddDays(7) : DateTimeOffset.UtcNow.AddMinutes(30) // Set expiration
                        };

                        // 4. Sign in the user
                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity),
                            authProperties);

                        // 5. Redirect to the original URL or a default page
                        if (Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        return RedirectToAction("Index", "Home"); // Redirect to Home page after login
                    }
                }
                // If user is null or password is invalid
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            // If ModelState is not valid or login fails, return the view with the model
            return View(model);
        }

        // GET: Users/Logout
        [HttpPost] // Logout should typically be a POST to prevent CSRF attacks
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home"); // Redirect to home or login page after logout
        }

        // ... (Your existing DeleteConfirmed, UserExists methods)

        //Gmail Authentication

        // GET: Users/ExternalLogin (Triggered by the "Sign in with Google" button)
        [HttpPost] // Typically triggered by a POST from a button
        [ValidateAntiForgeryToken] // Recommended for POST requests
        public IActionResult ExternalLogin(string provider, string? returnUrl = null)
        {
            // Construct the redirect URL for the callback
            // This is where Google will send the user back after they authenticate.
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Users", new { returnUrl });

            // Create a new AuthenticationProperties object to pass information to the authentication handler.
            // The RedirectUri is the most important property here; it tells the authentication handler
            // where to send the user after successful external authentication.
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };

            // The Challenge() method is available directly on the Controller base class.
            // It takes AuthenticationProperties and the authentication scheme (provider).
            // It returns a ChallengeResult, which tells ASP.NET Core to initiate the external login flow.
            return Challenge(properties, provider); // <-- Corrected line
        }


        // GET/POST: Users/ExternalLoginCallback (Called by Google after authentication)
        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback(string? returnUrl = null, string? remoteError = null)
        {
            if (remoteError != null)
            {
                // Handle errors from the external provider (e.g., user denied access)
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
                return View("Login"); // Redirect back to login page with error
            }

            // Get the information about the external login from the authentication handler
            var info = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

            if (info == null || !info.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "External login information not found or authentication failed.");
                return View("Login");
            }

            // Extract essential claims from the external provider's identity
            // The NameIdentifier claim holds the unique ID from Google
            var googleId = info.Principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = info.Principal.FindFirst(ClaimTypes.Email)?.Value;
            var name = info.Principal.FindFirst(ClaimTypes.Name)?.Value; // This is often full name

            if (string.IsNullOrEmpty(googleId) || string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError(string.Empty, "Could not retrieve essential user information from Google.");
                return View("Login");
            }

            // --- Your application's user management logic ---
            User user;

            // Option 1: Find user by Google ID (if you store it)
            user = await _context.Users.FirstOrDefaultAsync(u => u.GoogleId == googleId); // Assuming you add GoogleId to your User model

            if (user == null)
            {
                // Option 2: Find user by Email (if they might already have an account with the same email)
                user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

                if (user == null)
                {
                    // Case A: New user - create a new User account in your database
                    user = new User
                    {
                        UserName = email, // Or derive a unique username
                        Email = email,
                        FirstName = name?.Split(' ').FirstOrDefault() ?? "Guest", // Extract first name
                        LastName = name?.Split(' ').LastOrDefault() ?? "", // Extract last name
                        GoogleId = googleId, // Store the Google ID
                        UserType = "Customer", // Default type for new Google users
                        CreatedAt = DateTime.UtcNow,
                        // No password needed for Google authenticated users
                        Password = "EXTERNAL_LOGIN_NO_PASSWORD" // Set a placeholder or null if your DB allows
                    };
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    // Case B: Existing user, but first time signing in with Google.
                    // Link their existing account to their Google ID.
                    user.GoogleId = googleId;
                    _context.Update(user); // Mark user for update
                    await _context.SaveChangesAsync();
                    ModelState.AddModelError(string.Empty, "Your existing account has been linked with Google.");
                }
            }

            // --- Sign the user into your application using your application's cookie scheme ---
            var appClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName), // Use your app's username
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.UserType)
            };

            var appIdentity = new ClaimsIdentity(appClaims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true, // You might offer "Remember Me" here
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7) // Example expiration
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(appIdentity), authProperties);

            // Redirect to the original returnUrl or a default page
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home"); // Or another page like UserDashboard
        }
    }
}