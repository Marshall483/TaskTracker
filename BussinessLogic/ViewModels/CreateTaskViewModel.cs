using Models;
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ViewModels
{
    public class CreateTaskViewModel
    {
        [Required]
        [Display(Name = "Give the name for new task.")]
        [MinLength(3, ErrorMessage = "Required minimun 3 symbols.")]
        [MaxLength(50, ErrorMessage = "Constraint on maximun 50 symbols exceeded.")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Choose a task state.")]
        public string TaskState { get; set; }  
        public SelectList State { get; set; }

        public Guid ProjectGuid { get; set; }

    }
}
