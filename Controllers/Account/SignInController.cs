using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectVaila.Providers.EFCoreProvider.Data;

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
	}
}
