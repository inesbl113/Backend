using CLONETRELLOBACK.models;
using CLONETRELLOBACK.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CLONETRELLOBACK.Controllers
{
    [Route("api/list")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly ListDao _listDao;

        public ListController(ListDao listDao)
        {
            _listDao = listDao;
        }

        // GET: api/list
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<Lists>>> GetLists()
        {
            var lists = await _listDao.GetLists();
            return Ok(lists);
        }

        // GET: api/list/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lists>> GetList(int id)
        {
            var list = await _listDao.GetList(id);

            if (list == null)
            {
                return NotFound();
            }

            return Ok(list);
        }

        // PUT: api/list/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutList(int id, Lists list)
        {
            if (id != list.Id)
            {
                return BadRequest();
            }

            try
            {
                await _listDao.UpdateList(list);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
            {
                if (!_listDao.ListExists(id))
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

        // POST: api/list
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Lists>> PostList(Lists list)
        {
            await _listDao.AddList(list);
            return CreatedAtAction(nameof(GetList), new { id = list.Id }, list);
        }

        // DELETE: api/list/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteList(int id)
        {
            var list = await _listDao.GetList(id);
            if (list == null)
            {
                return NotFound();
            }

            var result = await _listDao.DeleteList(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
