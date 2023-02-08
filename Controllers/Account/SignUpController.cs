using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ProjectVaila.Providers.EFCoreProvider;
using ProjectVaila.Providers.EFCoreProvider.Data;
using ProjectVaila.ViewModels;

namespace ProjectVaila.Controllers.Account
{
	public class SignUpController : Controller
	{
		private readonly UserManager<IdentityUserContext> _userManager;
		private readonly SignInManager<IdentityUserContext> _signInManager;
		private readonly AppDbContext _appDbContext;

		public SignUpController(UserManager<IdentityUserContext> userManager, SignInManager<IdentityUserContext> signInManager, AppDbContext appDbContext)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_appDbContext = appDbContext;
		}

		[HttpGet]
		public IActionResult Index()
		{
			ViewBag.PageTitle = "Sign Up";
			ViewBag.DisplayHeader = false;

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Index(SignUpViewModel model)
		{
			ViewBag.PageTitle = "Sign Up";
			ViewBag.DisplayHeader = false;

			if (!ModelState.IsValid) return View(model);

			var identityUser = new IdentityUserContext
			{
				UserName = string.Join("", model.Email.TakeWhile(c => c != '@')),
				Email = model.Email,
			};
				
			var addUserResult = await _userManager.CreateAsync(identityUser, model.Password);

			if (addUserResult.Succeeded)
			{
				await _signInManager.SignInAsync(identityUser, false);

				var user = new User()
				{
					Id = _userManager.Users.Where(u => u.Email == model.Email).Select(u => u.Id).First(),
					FirstName = model.FirstName,
					LastName = model.LastName,
				};

				await _appDbContext.AddAsync(user);
				
				return RedirectToAction("Index", "Home");
			}

			foreach (var error in addUserResult.Errors)
			{
				ModelState.AddModelError(string.Empty, error.Description);
			}

			return View(model);
		}
	}
}
