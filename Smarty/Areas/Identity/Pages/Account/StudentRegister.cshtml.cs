using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Smarty.Data.Enums;
using Smarty.Data.Models;
using Smarty.Data.ViewModels.Students;

namespace Smarty.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class StudentRegisterModel : PageModel
    {
        private readonly SignInManager<SmartyUser> _signInManager;
        private readonly UserManager<SmartyUser> _userManager;
        private readonly ILogger<StudentRegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;

        public StudentRegisterModel(
            UserManager<SmartyUser> userManager,
            SignInManager<SmartyUser> signInManager,
            ILogger<StudentRegisterModel> logger,
            IEmailSender emailSender,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
			_mapper = mapper;
		}

        [BindProperty]
        public StudentRegisterFormViewModel ViewModel { get; set; }

        public string ReturnUrl { get; set; }



		public  void OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            if (ModelState.IsValid)
			{
				var user = _mapper.Map<SmartyUser>(ViewModel);
				
				var result = await _userManager.CreateAsync(user, ViewModel.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    await _userManager.AddToRoleAsync(user, Roles.Student.ToString());
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(ViewModel.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = ViewModel.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
