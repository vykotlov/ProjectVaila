using System.ComponentModel.DataAnnotations;

namespace ProjectVaila.ViewModels
{
	public class SignInViewModel
	{
		[Required(AllowEmptyStrings = false, ErrorMessage = "Введите корректный Email")]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "Введите пароль")]
		[DataType(DataType.Password)]
		[Display(Name = "Пароль")]
		public string Password { get; set; }

		[Display(Name = "Запомнить?")]
		public bool RememberMe { get; set; }
	}
}
