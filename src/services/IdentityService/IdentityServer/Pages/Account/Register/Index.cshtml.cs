using System.Security.Claims;
using IdentityModel;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityServer.Pages.Account.Register;

public class IndexModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IndexModel(
            UserManager<User> userManager,
                SignInManager<User> signInManager,
                RoleManager<IdentityRole> roleInManager
              )
        {
            _roleManager = roleInManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [BindProperty]
        public RegisterViewModel Input { get; set; }


        public async Task<IActionResult> OnGet(string returnUrl)
        {
            
            Input = new RegisterViewModel
            {
                ReturnUrl = returnUrl
            };
            return Page();
        }

        public async Task<IActionResult> OnPost(string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    EmailConfirmed = true,
                    NickName = Input.NickName
                };

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    /*if (!_roleManager.RoleExistsAsync(Input.RoleName).GetAwaiter().GetResult())
                    {
                        var userRole = new IdentityRole
                        {
                            Name = Input.RoleName,
                            NormalizedName = Input.RoleName,

                        };
                        await _roleManager.CreateAsync(userRole);
                    }
                    await _userManager.AddToRoleAsync(user, Input.RoleName);*/


                    await _userManager.AddClaimsAsync(user, new Claim[] {
                        new Claim(JwtClaimTypes.Name,Input.Email),
                        new Claim(JwtClaimTypes.Email,Input.Email),
                        new Claim(JwtClaimTypes.NickName, Input.NickName)
                        //new Claim(JwtClaimTypes.Role,Input.RoleName)
                    });

                    return RedirectToPage("Login", returnUrl);
                    var loginresult = await _signInManager.PasswordSignInAsync(
                        Input.Email, Input.Password, false, lockoutOnFailure: true);

                    if (loginresult.Succeeded)
                    {
                        if (Url.IsLocalUrl(Input.ReturnUrl))
                        {
                            return Redirect(Input.ReturnUrl);
                        }
                        else if (string.IsNullOrEmpty(Input.ReturnUrl))
                        {
                            return Redirect("~/");
                        }
                        else
                        {
                            throw new Exception("invalid return URL");
                        }

                    }

                }

            }
            return Page();
        }
    }
    