using System.ComponentModel.DataAnnotations;

namespace ProjectVaila.ViewModels
{
	public class SignUpViewModel
	{
		[Required(AllowEmptyStrings = false, ErrorMessage = "Введите Email")]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "Введите имя")]
		[Display(Name = "Имя")]
		public string FirstName { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "Введите фамилию")]
		[Display(Name = "Фамилия")]
		public string LastName { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "Введите пароль")]
		[DataType(DataType.Password)]
		[Display(Name = "Пароль")]
		public string Password { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "Повторите пароль")]
		[Compare("Password", ErrorMessage = "Пароли не совпадают")]
		[DataType(DataType.Password)]
		[Display(Name = "Подтвердить пароль")]
		public string PasswordConfirm { get; set; }
	}
}
