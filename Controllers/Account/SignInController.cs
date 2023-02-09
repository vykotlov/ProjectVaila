using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectVaila.Providers.EFCoreProvider.Data;
using ProjectVaila.ViewModels;

namespace ProjectVaila.Controllers.Account
{
	public class SignInController : Controller
	{
		private readonly UserManager<IdentityUserContext> _userManager;
		private readonly SignInManager<IdentityUserContext> _signInManager;

		public SignInController(UserManager<IdentityUserContext> userManager, SignInManager<IdentityUserContext> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[HttpGet]
		public IActionResult Index()
		{
			ViewBag.PageTitle = "Sign In";
			ViewBag.DisplayHeader = false;

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Index(SignInViewModel model)
		{
			ViewBag.PageTitle = "Sign In";
			ViewBag.DisplayHeader = false;

			if (!ModelState.IsValid) return View(model);

			var user = await _userManager.FindByEmailAsync(model.Email);

			if (user is not null)
			{
				var result =
					await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);

				if (result.Succeeded)
				{
					return RedirectToAction("Index", "Home");
				}
			}

			ModelState.AddModelError("", "Неправильный email и (или) пароль");

			return View(model);
		}
	}
}
