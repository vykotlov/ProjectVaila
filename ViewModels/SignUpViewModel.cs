﻿using System.ComponentModel.DataAnnotations;

namespace ProjectVaila.ViewModels
{
	public class SignUpViewModel
	{
		[Required]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[Required]
		[Display(Name = "Имя")]
		public string FirstName { get; set; }

		[Required]
		[Display(Name = "Фамилия")]
		public string LastName { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Пароль")]
		public string Password { get; set; }

		[Required]
		[Compare("Password", ErrorMessage = "Пароли не совпадают")]
		[DataType(DataType.Password)]
		[Display(Name = "Подтвердить пароль")]
		public string PasswordConfirm { get; set; }
	}
}
