using Models;
using System;
using System.ComponentModel.DataAnnotations;


namespace ViewModels
{
    public class ProjectViewModel
    {
        [Required]
        [Display(Name = "Give name for new project.")]
        [MinLength(3, ErrorMessage = "Required minimun 3 symbols.")]
        [MaxLength(50, ErrorMessage = "Constraint on maximun 50 symbols exceeded.")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Project priority.")]
        [Range(1, 3, ErrorMessage = "1 - Low, 2 - Normal, 3 - High.")] 
        public int Priority { get; set; }

        [Required]
        [Display(Name = "Choose project state.")]
        public ProjectState State { get; set; }

        [Required]
        [Display(Name = "Select start date.")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "Select competition date.")]
        [DataType(DataType.Date)]
        public DateTime CompetitionDate { get; set; }

    }
}
