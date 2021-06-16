using System;
using System.Collections.Generic;
using System.Text;
using Services.Abstractions;
using Models;
using System.Threading.Tasks;

namespace Services 
{
    public class TaskService : IProjectService<ProjectTask>
    {
        public Task<ProjectTask> Create()
        {
            throw new NotImplementedException();
        }

        public Task<ProjectTask> Delete()
        {
            throw new NotImplementedException();
        }

        public Task<ProjectTask> Edit()
        {
            throw new NotImplementedException();
        }

        public Task<ProjectTask> View()
        {
            throw new NotImplementedException();
        }
    }
}
