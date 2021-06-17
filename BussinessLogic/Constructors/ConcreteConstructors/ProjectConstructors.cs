using System;
using System.Collections.Generic;
using System.Text;
using Abstractions;
using Models;
using ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Constructors
{
    public class CreateProjectConstructor 
        : IConstructor<CreateProjectViewModel, Project>
    {
        public CreateProjectViewModel ConsructView(Project project)
        {
            var model = new CreateProjectViewModel();

            model.StateSelect = new SelectList(new[] { "Not Started", "Active", "Completed" });
            model.PrioritySelect = new SelectList(new[] { "Low", "Normal", "High" });

            return model;
        }
    }

    public class EditProjectConstructor 
        : IConstructor<EditProjectViewModel, Project>
    {
        public readonly Dictionary<ProjectState, string> _stateMap =
            new Dictionary<ProjectState, string>
            {
                {ProjectState.NotStarted, "Not started" },
                {ProjectState.Active, "Active" },
                {ProjectState.Completed, "Completed" } 
            };

        public readonly Dictionary<int, string> _priorityMap =
            new Dictionary<int, string>
            {
                { 1, "Low" },
                { 2, "Normal" },
                { 3, "High" },
            };

        public EditProjectViewModel ConsructView(Project project)
        {
            var model = new EditProjectViewModel();

            model.Name = project.ProjectName ?? default;
            model.Priority = _priorityMap[project.Priority];
            model.State = _stateMap[project.State];
            model.StartDate = project.StartDate;
            model.CompetitionDate = project.CompetitionDate;

            return model;
        }
    }
}
