using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class User : IdentityUser<Guid>
    {
        [Key]
        public override Guid Id { get; set; }
        public ICollection<Project> Projects { get; set; }

    }
}
