using Microsoft.AspNet.Mvc;
using System.Linq;
using Microsoft.AspNet.Mvc.ModelBinding;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace vNextTodoApp.Controllers
{
    public class TasksController : BaseController
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

        public async System.Threading.Tasks.Task<IActionResult> Post([Body]TaskInput input)
        {
            input = new TaskInput();
            if (!await TryUpdateModelAsync(input))
                return new HttpStatusCodeResult(400);

            if (!ModelState.IsValid)
                return new HttpStatusCodeResult(400);


            var newTask = new Task();
            _dbContext.Tasks.Add(newTask);
            await _dbContext.SaveChangesAsync();

            return Json(new { Id = newTask.TaskId, Description = newTask.Description, IsCompleted = newTask.IsCompleted });
        }


        [HttpGet]
        public IActionResult Test()
        {
            return View(new TaskInput());
        }

        [HttpPost]
        public IActionResult Test(TaskInput input)
        {
            return View(input);
        }
    }
}
