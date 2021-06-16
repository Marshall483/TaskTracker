using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IProjectService<TResult>
    {
        public Task<TResult> Create();
        public Task<TResult> View();
        public Task<TResult> Edit();
        public Task<TResult> Delete();

    }
}
