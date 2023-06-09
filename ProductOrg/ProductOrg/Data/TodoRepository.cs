using ProductOrg.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProductOrg.Repositories
{
    public class TodoRepository
    {
        #region Public Methods

        public List<TodoModel> LoadAsync()
        {
            List<TodoModel> todos = null;

            using (var context = new ProductOrgContext())
            {
                todos = context.todoModels.ToList();

                if (todos.Count == 0)
                {
                    todos.Add(new TodoModel() { Id = 0, Title = "Example", Description = "Description example", IsDone = false, Date = System.DateTime.Now });
                }
            }

            return todos;
        }

        public async void SaveAsync(TodoModel todo)
        {
            using (var context = new ProductOrgContext())
            {
                context.todoModels.Add(todo);
                await context.SaveChangesAsync();
            }
        }

        public async void UpdateAsync(TodoModel todo)
        {
            TodoModel todoLocal = null;

            using (var context = new ProductOrgContext())
            {
                todoLocal = context.todoModels.FirstOrDefault(t => t.Id == todo.Id);
                todoLocal.Title = todo.Title;
                todoLocal.Description = todo.Description;
                todoLocal.IsDone = todo.IsDone;
                await context.SaveChangesAsync();
            }
        }

        #endregion
        #region Private Methods

        private async void DeleteAsync()
        {
            using (var context = new ProductOrgContext())
            {
                var configs = context.todoModels.ToList();
                context.todoModels.RemoveRange(configs);
                await context.SaveChangesAsync();
            }
        }

        #endregion
    }
}
