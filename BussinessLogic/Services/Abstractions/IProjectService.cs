using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Abstractions
{
    public interface IProjectService<TResult, TProject>
    {
        public Task<TResult> Create(TProject project);
        public Task<TResult> View(string id);
        public Task<TResult> Edit();
        public Task<TResult> Delete();

    }
}