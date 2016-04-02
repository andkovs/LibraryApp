using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryApp.Models
{
    public class UserRegistryModel
    {        
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(20, ErrorMessage = "Maximum 20 chars")]
        [Display(Name = "Логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(20, ErrorMessage = "Maximum 20 chars")]
        [MinLength(5, ErrorMessage = "Minimum 5 chars")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(20, ErrorMessage = "Maximum 20 chars")]
        [MinLength(5, ErrorMessage = "Minimum 5 chars")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Повторите пароль")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(20, ErrorMessage = "Maximum 50 chars")]
        [MinLength(5, ErrorMessage = "Minimum 8 chars")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

    }

    public class UserAuthorizationModel
    {
		[Required(ErrorMessage = "Name is required")]
		[MaxLength(20, ErrorMessage = "Maximum 20 chars")]
		[Display(Name = "Логин")]
        public string Login { get; set; }

		[Required(ErrorMessage = "Password is required")]
		[MaxLength(20, ErrorMessage = "Maximum 20 chars")]
		[MinLength(4, ErrorMessage = "Minimum 4 chars")]
		[DataType(DataType.Password)]
		[Display(Name = "Пароль")]
        public string Password { get; set; }

    }

    public class UserAccountModel
    {
        public int Id { get; set; }

        public String Login { get; set; }        

        public String Name { get; set; }

        public String LastName { get; set; }

        public String Email { get; set; }

        public String AvatarUrl { get; set; }
    }
}