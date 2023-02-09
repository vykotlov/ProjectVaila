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
				UserName = model.Email,
				Email = model.Email,
			};
				
			var addUserResult = await _userManager.CreateAsync(identityUser, model.Password);

			if (addUserResult.Succeeded)
			{
				await _signInManager.SignInAsync(identityUser, false);

				var identityUserContext = await _userManager.FindByEmailAsync(model.Email);

				var user = new User()
				{
					Id = identityUserContext.Id,
					FirstName = model.FirstName,
					LastName = model.LastName,
				};

				await _appDbContext.AddAsync(user);
				
				return RedirectToAction("Index", "Home");
			}

			foreach (var error in addUserResult.Errors)
			{
				string errorDesc = error.Description;

				//todo: разобраться как заменить switch на более лакончиный код
				switch (error.Code)
				{
					case "PasswordTooShort":
						errorDesc = "Пароль должен содержать как минимум 6 символов";
						break;
					case "PasswordRequiresNonAlphanumeric":
						errorDesc = "Пароль должен содержать как минимум один символ не являющийся цифробуквенным";
						break;
					case "PasswordRequiresLower":
						errorDesc = "Пароль должен содержать как минимум один символ в нижнем регистре ";
						break;
					case "PasswordRequiresUpper":
						errorDesc = "Пароль должен содержать как минимум один символ в верхнем регистре ";
						break;
				}

				ModelState.AddModelError(string.Empty, errorDesc);
			}

			return View(model);
		}
	}
}
