// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Joinler.Data.Models;
using Joinler.Data.Models.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace Joinler.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<UserProfile> _signInManager;
        private readonly UserManager<UserProfile> _userManager;
        private readonly IUserStore<UserProfile> _userStore;
        private readonly IUserEmailStore<UserProfile> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<UserProfile> userManager,
            IUserStore<UserProfile> userStore,
            SignInManager<UserProfile> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            //TODO
            [Required]
            [Display(Name = "Username")]
            [MinLength(3)]
            [MaxLength(25)]
            [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Username must be combination of letters and numbers only.")]
            public string UserName { get; set; }

            [Required]
            [MinLength(3)]
            [MaxLength(25)]
            public string FirstName { get; set; }

            [Required]
            [MinLength(3)]
            [MaxLength(25)]
            public string LastName { get; set; }

            [Required]
            public Gender Gender { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            this.ReturnUrl = returnUrl;
            this.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            this.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (this.ModelState.IsValid)
            {
                var user = CreateUser();

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(
                        this.Input.Email, 
                        "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }

                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }

        private UserProfile CreateUser()
        {
            var user = new UserProfile
            {
                UserName = this.Input.UserName,
                Email = this.Input.Email,
                FirstName = this.Input.FirstName,
                LastName = this.Input.LastName,
                Gender = this.Input.Gender,
            };

            //try
            //{
            //    return Activator.CreateInstance<UserProfile>();
            //}
            //catch
            //{
            //    throw new InvalidOperationException($"Can't create an instance of '{nameof(UserProfile)}'. " +
            //        $"Ensure that '{nameof(UserProfile)}' is not an abstract class and has a parameterless constructor, or alternatively " +
            //        $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            //}

            return user;
        }

        private IUserEmailStore<UserProfile> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<UserProfile>)_userStore;
        }
    }
}
