﻿using Models;
using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class TaskFieldViewModel
    {
        [Required]
        [Display(Name = "Well, what you want to do?.")]
        [MinLength(3, ErrorMessage = "Required minimun 3 symbols.")]
        [MaxLength(5000, ErrorMessage = "Constraint on maximun 5000 symbols exceeded.")]
        public string Descrition { get; set; }
    }
}
