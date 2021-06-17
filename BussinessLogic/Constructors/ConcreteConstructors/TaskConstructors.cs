using Abstractions;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Constructors
{
    public class CreateTaskConstructor :
         ProjectConstructorBase,
         IConstructor<CreateTaskViewModel, ProjectTask>
    {
        public CreateTaskViewModel ConsructView(ProjectTask task)
        {
            var model = new CreateTaskViewModel();

            model.Name = task.TaskName;
            model.TaskState = _taskStateByEnumMap[task.State];
            model.State = new SelectList(_taskStateByStringMap.Keys);

            return model;
        }

        public ProjectTask ConstructModel(CreateTaskViewModel taskView)
        {
            var task = new ProjectTask();

            task.TaskName = taskView.Name;
            task.State = _taskStateByStringMap[taskView.TaskState];
            task.ProjectId = taskView.ProjectGuid;

            return task;
        }
    }

    public class EditTaskConstructor :
        ProjectConstructorBase,
        IConstructor<EditTaskViewModel, ProjectTask>
    {
        public EditTaskViewModel ConsructView(ProjectTask task)
        {
            var model = new EditTaskViewModel();

            model.Name = task.TaskName;
            model.TaskState = _taskStateByEnumMap[task.State];
            model.State = new SelectList(_taskStateByStringMap.Keys);
            model.ProjectGuid = task.ProjectId;

            return model;
        }

        public ProjectTask ConstructModel(EditTaskViewModel taskView)
        {
            var task = new ProjectTask();

            task.TaskName = taskView.Name;
            task.State = _taskStateByStringMap[taskView.TaskState];

            return task;
        }
    }
}
