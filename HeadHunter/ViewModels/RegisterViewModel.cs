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
        [Display(Name = "��� ������������")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "����� ��������")]
        public string PhoneNumber { get; set; }
        [Required]
        [Display(Name = "��� ������������")]
        public Occupation Occupation { get; set; }
        [Display(Name = "������")]
        public string Avatar { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "������")]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "������ �� ���������")]
        [DataType(DataType.Password)]
        [Display(Name = "����������� ������")]
        public string PasswordConfirm { get; set; }
    }
}
