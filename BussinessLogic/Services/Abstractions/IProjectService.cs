using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Abstractions
{
    public interface IProjectService<TResult, TCreate, TEdit>
    {
        public Task<TResult> Create(TCreate model);
        public Task<TResult> View(string id);
        public Task<TResult> Edit(TEdit model);
        public Task<TResult> Delete(string guid);

    }
}