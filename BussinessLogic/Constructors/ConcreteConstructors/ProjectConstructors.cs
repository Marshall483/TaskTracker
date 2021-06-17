using System;
using System.Collections.Generic;
using System.Text;
using Abstractions;
using Models;
using ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Constructors
{
    public class CreateProjectConstructor :
        ProjectConstructorBase,
        IConstructor<CreateProjectViewModel, Project>
    {
        public CreateProjectViewModel ConsructView(Project project)
        {
            var model = new CreateProjectViewModel();

            model.StateSelect = new SelectList(_stateByStringMap.Keys);
            model.PrioritySelect = new SelectList(_priorityByStringMap.Keys);

            return model;
        }

        public Project ConstructModel(CreateProjectViewModel model)
        {
            var project = new Project();

            project.ProjectName = model.Name;
            project.Priority = _priorityByStringMap[model.Priority];
            project.State = _stateByStringMap[model.State];
            project.StartDate = model.StartDate;
            project.CompetitionDate = model.CompetitionDate;
            project.UserId = model.UserGuid;

            return project;
        }
    }

    public class EditProjectConstructor :
        ProjectConstructorBase,
        IConstructor<EditProjectViewModel, Project>
    {

        public EditProjectViewModel ConsructView(Project project)
        {
            var model = new EditProjectViewModel();

            model.Name = project.ProjectName;
            model.Priority = _priorityByIntMap[project.Priority];
            model.State = _stateByEnumMap[project.State];
            model.StartDate = project.StartDate;
            model.CompetitionDate = project.CompetitionDate;
            model.StateSelect = new SelectList(_stateByStringMap.Keys);
            model.PrioritySelect = new SelectList(_priorityByStringMap.Keys);

            return model;
        }

        public Project ConstructModel(EditProjectViewModel model)
        {
            var project = new Project();

            project.ProjectName = model.Name;
            project.Priority = _priorityByStringMap[model.Priority];
            project.State = _stateByStringMap[model.State];
            project.StartDate = model.StartDate;
            project.CompetitionDate = model.CompetitionDate;

            return project;
        }
    }
}
