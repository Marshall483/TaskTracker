using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.ComponentModel.DataAnnotations;


namespace ViewModels
{
    public class CreateProjectViewModel
    {
        [Required]
        [Display(Name = "Give name for new project.")]
        [MinLength(3, ErrorMessage = "Required minimun 3 symbols.")]
        [MaxLength(50, ErrorMessage = "Constraint on maximun 50 symbols exceeded.")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Choose project priority.")]
        public string Priority { get; set; }
        public SelectList PrioritySelect { get; set; }

        [Required]
        [Display(Name = "Choose project state.")]
        public string State { get; set; }
        public SelectList StateSelect { get; set; }

        [Required]
        [Display(Name = "Select start date.")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "Select competition date.")]
        [DataType(DataType.Date)]
        public DateTime CompetitionDate { get; set; }

        public Guid UserGuid { get; set; }
    }
}
