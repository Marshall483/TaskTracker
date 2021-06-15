using Models;
using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class TaskViewModel
    {
        [Required]
        [Display(Name = "Give name for a task.")]
        [MinLength(3, ErrorMessage = "Required minimun 3 symbols.")]
        [MaxLength(50, ErrorMessage = "Constraint on maximun 50 symbols exceeded.")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Choose a task state.")]
        public TaskState State { get; set; }

    }
}
