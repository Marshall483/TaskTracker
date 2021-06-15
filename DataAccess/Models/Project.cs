using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Project
    {
        public Guid Id { get; set; }

        public ICollection<ProjectTask> MyProperty { get; set; }
    }
}
