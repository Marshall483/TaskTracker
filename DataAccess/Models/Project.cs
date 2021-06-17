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

        public Guid UserId { get; set; }
        public User User { get; set; }

        [Required]
        public string ProjectName { get; set; }

        // Must be private, but i cant map it to db.
        [Required]
        public int intPriority { get; set; }

        public ICollection<ProjectTask> Tasks { get; set; }

        [Required]
        public ProjectState State { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
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
            get => intPriority;
            set
            {
                //   Low            Normal        High         
                if (value == 1 || value == 2 || value == 3)
                    intPriority = value;
                else
                    intPriority = 2;
            }
        }
    }

}
