using CLONETRELLOBACK.models;
using CLONETRELLOBACK.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CLONETRELLOBACK.Controllers
{
    [Route("api/project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectDao _projectDao;

        public ProjectController(ProjectDao projectDao)
        {
            _projectDao = projectDao;
        }

        // GET: api/project
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<Projects>>> GetProjects()
        {
            var projects = await _projectDao.GetProjects();
            return Ok(projects);
        }

        // GET: api/project/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Projects>> GetProject(int id)
        {
            var project = await _projectDao.GetProject(id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        // PUT: api/project/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, Projects project)
        {
            if (id != project.Id)
            {
                return BadRequest();
            }

            try
            {
                await _projectDao.UpdateProject(project);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
            {
                if (!_projectDao.ProjectExists(id))
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

        // POST: api/project
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Projects>> PostProject(Projects project)
        {
            await _projectDao.AddProject(project);
            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
        }

        // DELETE: api/project/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _projectDao.GetProject(id);
            if (project == null)
            {
                return NotFound();
            }

            var result = await _projectDao.DeleteProject(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
