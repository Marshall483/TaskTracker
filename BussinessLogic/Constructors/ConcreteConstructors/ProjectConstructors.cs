using System;
using System.Collections.Generic;
using System.Text;
using Abstractions;
using Models;
using ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Constructors
{
    public abstract class ProjectConstructorBase
    {
        public readonly Dictionary<ProjectState, string> _stateByEnumMap =
           new Dictionary<ProjectState, string>
           {
                {ProjectState.NotStarted, "Not started" },
                {ProjectState.Active, "Active" },
                {ProjectState.Completed, "Completed" }
           };

        public readonly Dictionary<string, ProjectState> _stateByStringMap =
          new Dictionary<string, ProjectState>
          {
                { "Not started", ProjectState.NotStarted },
                { "Active", ProjectState.Active },
                { "Completed", ProjectState.Completed }
          };

        public readonly Dictionary<int, string> _priorityByIntMap =
            new Dictionary<int, string>
            {
                { 1, "Low" },
                { 2, "Normal" },
                { 3, "High" },
            };

        public readonly Dictionary<string, int> _priorityByStringMap =
            new Dictionary<string, int>
            {
                { "Low", 1 },
                { "Normal", 2 },
                { "High", 3 },
            };
    }

    public class CreateProjectConstructor :
        ProjectConstructorBase,
        IConstructor<CreateProjectViewModel, Project>
    {
        public CreateProjectViewModel ConsructView(Project project)
        {
            var viewModel = new CreateProjectViewModel();

            viewModel.StateSelect = new SelectList(_stateByStringMap.Keys);
            viewModel.PrioritySelect = new SelectList(_priorityByStringMap.Keys);

            return viewModel;
        }

        public Project ConstructModel(CreateProjectViewModel model)
        {
            var domainProjectModel = new Project();

            domainProjectModel.ProjectName = model.Name;
            domainProjectModel.Priority = _priorityByStringMap[model.Priority];
            domainProjectModel.State = _stateByStringMap[model.State];
            domainProjectModel.StartDate = model.StartDate;
            domainProjectModel.CompetitionDate = model.CompetitionDate;
            domainProjectModel.UserId = model.UserGuid;

            return domainProjectModel;
        }
    }

    public class EditProjectConstructor 
        : ProjectConstructorBase, 
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

            return model;
        }

        public Project ConstructModel(EditProjectViewModel model)
        {
            throw new NotFiniteNumberException();
        }
    }
}
