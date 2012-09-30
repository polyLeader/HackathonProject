using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace PolyTeam.Hackaton.Models
{

    public class LogOnModel
    {
        [Required]
        [Display(Name = "Ім'я користувача")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запам'ятати?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "Логін")]
        public string Login { get; set; }

        [Required]
        [Display(Name = "Прізвище")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Ім'я")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "По батькові")]
        public string SecondName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} повинно бути мінімум {2} символи", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Підтвердити пароль")]
        [Compare("Password", ErrorMessage = "Пароль та підтвердження не співпадають")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Вулиця")]
        public string UserStreet { get; set; }

        [Required]
        [Display(Name = "Дім")]
        public string UserHouse { get; set; }

        [Required]
        [Display(Name = "Квартира")]
        public string UserFlat { get; set; }

        [Required]
        [Display(Name = "Телефон")]
        public string UserPhoneNumber { get; set; }
    }
}