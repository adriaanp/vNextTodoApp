using Microsoft.AspNet.Mvc;
using System.Linq;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace vNextTodoApp.Controllers
{
    public class TasksController : Controller
    {
        private TaskDbContext _dbContext;

        public TasksController(TaskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Get()
        {
            var tasks = _dbContext.Tasks.Select(t => new { Id = t.TaskId, t.Description, t.IsCompleted }).ToList();
            return Json(tasks);
        }

        public IActionResult Post(TaskInput input)
        {
            if (!ModelState.IsValid)
                return new HttpStatusCodeResult(400);

            var newTask = new Task() { Description = input.Description };

            _dbContext.Tasks.Add(newTask);
            _dbContext.SaveChanges();

            return Json(new { Id = newTask.TaskId, Description = newTask.Description, IsCompleted = newTask.IsCompleted });
        }
    }
}
