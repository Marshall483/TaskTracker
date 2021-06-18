using Abstractions;
using DataAccess;
using Models;
using Monads;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViewModels;

namespace Realizations
{
    class FieldService : IFieldService<Either<TaskFields, ICollection<Error>>>
    {
        private readonly Database _db;

        public FieldService(Database db)
        {
            _db = db;
        }


        public async Task<Either<TaskFields, ICollection<Error>>> AddField(AddTaskFieldViewModel fieldModel)
        {
            var field = new TaskFields();

            field.Description = fieldModel.Descrition;
            field.TaskId = fieldModel.TaskGuid;

            _db.TaskFields.Add(field);
            var inserted = await _db.SaveChangesAsync();

            if (inserted > 0)
                return Success(field);
            else
                return Error("Field not added.");
        }

        public async Task<Either<TaskFields, ICollection<Error>>> DeleteField(string fieldGuid)
        {
            var field = _db.TaskFields.Find(Guid.Parse(fieldGuid));

            _db.TaskFields.Remove(field);
            var deleted = await _db.SaveChangesAsync();

            if (deleted > 0)
                return Success(new TaskFields());
            else
                return Error("Field not deleted.");
        }

        public async Task<Either<TaskFields, ICollection<Error>>> EditField(EditFieldViewModel fieldModel)
        {
            var field = _db.TaskFields.Find(fieldModel.FieldGuid);
            field.Description = fieldModel.Descrition;

            _db.TaskFields.Update(field);
            var updated = await _db.SaveChangesAsync();

            if (updated > 0)
                return Success(field);
            else
                return Error("Field not edited.");
        }

        private static Either<TaskFields, ICollection<Error>> Error(string error) =>
            Either<TaskFields, ICollection<Error>>.
                    WithError(new Error[] { error });

        private static Either<TaskFields, ICollection<Error>> Success(TaskFields field) =>
           Either<TaskFields, ICollection<Error>>.
                   WithSuccess(field);
    }
}
