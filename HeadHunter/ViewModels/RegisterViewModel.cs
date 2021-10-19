using HeadHunter.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HeadHunter.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Род деятельности")]
        public Occupation Occupation { get; set; }
        [Display(Name = "Аватар")]
        public string Avatar { get; set; }
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
