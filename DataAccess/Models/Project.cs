using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Models
{
    public enum ProjectState : byte
    {
        NotStarted = 1, 
        Active = 2,
        Completed = 3
    }

    public class Project
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string ProjectName { get; set; }

        [Required]
        private int _priority;

        public ICollection<ProjectTask> Tasks { get; set; }

        [Required]
        public ProjectState State { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime StartDate { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTime CompetitionDate { get; set; }

        [NotMapped]
        public bool AnyTask => 
            Tasks != null &&
            Tasks.Any();

        [NotMapped]
        public int Priority
        {
            get => _priority;
            set
            {
                //   Low            Normal        High         
                if (value == 1 || value == 2 || value == 3)
                    _priority = value;
                else
                    _priority = 2;
            }
        }
    }

}
