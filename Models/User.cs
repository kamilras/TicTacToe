using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicTacToe.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Imię")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Display(Name = "Potwierdź hasło")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Wynik")]
        public int Score { get; set; }


    }
}