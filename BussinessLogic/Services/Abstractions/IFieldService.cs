using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Abstractions
{
    public interface IFieldService<TResult>
    {
        public Task<TResult> AddField(AddTaskFieldViewModel field);
        public Task<TResult> DeleteField(string fieldId);
        public Task<TResult> EditField(EditFieldViewModel task);
    }
}
