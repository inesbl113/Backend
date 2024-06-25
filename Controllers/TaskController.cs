using CLONETRELLOBACK.models;
using CLONETRELLOBACK.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task = CLONETRELLOBACK.models.Tasks;

namespace CLONETRELLOBACK.Controllers
{
    [Route("api/task")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TasksDao _tasksDao;

        public TaskController(TasksDao tasksDao)
        {
            _tasksDao = tasksDao;
        }

        // GET: api/task
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<Task>>> GetTasks()
        {
            var tasks = await _tasksDao.GetTasks();
            return Ok(tasks);
        }

        // GET: api/task/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Task>> GetTask(int id)
        {
            var task = await _tasksDao.GetTask(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        // PUT: api/task/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id, Task task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }

            try
            {
                await _tasksDao.UpdateTask(task);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
            {
                if (!_tasksDao.TaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/task
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Task>> PostTask(Task task)
        {
            await _tasksDao.AddTask(task);
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        // DELETE: api/task/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _tasksDao.GetTask(id);
            if (task == null)
            {
                return NotFound();
            }

            var result = await _tasksDao.DeleteTask(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
