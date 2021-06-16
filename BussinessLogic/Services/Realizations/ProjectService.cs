using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Models;
using Services.Abstractions;

namespace Services
{
    public class ProjectService : IProjectService<Project>
    {

        public Task<Project> Create()
        {
            throw new NotImplementedException();
        }

        public Task<Project> View()
        {
            throw new NotImplementedException();
        }

        public Task<Project> Edit()
        {
            throw new NotImplementedException();
        }

        public Task<Project> Delete()
        {
            throw new NotImplementedException();
        }
    }
}
