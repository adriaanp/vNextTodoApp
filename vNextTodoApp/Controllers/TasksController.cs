﻿using Microsoft.AspNet.Mvc;
using System.Linq;
using Microsoft.AspNet.Mvc.ModelBinding;
using System.Threading.Tasks;

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

        public async System.Threading.Tasks.Task<IActionResult> Post([FromBody]TaskInput input)
        {
            //input = new TaskInput();

            //if (!await TryUpdateModelAsync(input))
              //  return new HttpStatusCodeResult(400);

            //[FromBody] does not valid model at the moment.
            if (!ModelState.IsValid)
                return new HttpStatusCodeResult(400);


            var newTask = new Task();
            newTask.Description = input.Description;
            newTask.IsCompleted = input.IsCompleted;
            _dbContext.Tasks.Add(newTask);
            await _dbContext.SaveChangesAsync();

            return Json(new { Id = newTask.TaskId, Description = newTask.Description, IsCompleted = newTask.IsCompleted });
        }

        public async Task<IActionResult> Put(int id, [FromBody]TaskInput input)
        {
            //TODO: not there yet
            if (!ModelState.IsValid)
                return new HttpStatusCodeResult(400);

            //var task = _dbContext.Tasks.Find()
            var task = await _dbContext.Tasks.FirstOrDefaultAsync(tsk => tsk.TaskId == id);

            if (task == null)
                return new HttpStatusCodeResult(404);

            task.Description = input.Description;
            task.IsCompleted = input.IsCompleted;

            await _dbContext.SaveChangesAsync();

            return Json(new { Id = task.TaskId, Description = task.Description, IsCompleted = task.IsCompleted });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var task = await _dbContext.Tasks.FirstOrDefaultAsync(tsk => tsk.TaskId == id);
            if (task == null)
            {
                return new HttpStatusCodeResult(404);
            }

            _dbContext.Tasks.Remove(task);
            await _dbContext.SaveChangesAsync();

            return new HttpStatusCodeResult(200);
        }

        [HttpGet]
        public IActionResult Test()
        {
            return View(new TaskInput());
        }

        [HttpPost]
        public IActionResult Test(TaskInput input)
        {
            if (!ModelState.IsValid)
            {
                input.Description = "must be something";
                return View(input);
            }
            return View(input);
        }
    }
}
