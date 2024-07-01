using CLONETRELLOBACK.models;
using CLONETRELLOBACK.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CLONETRELLOBACK.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly CommentDao _commentDao;

        public CommentController(CommentDao commentDao)
        {
            _commentDao = commentDao;
        }

        // GET: api/comment
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<Comments>>> GetComments()
        {
            var comments = await _commentDao.GetComments();
            return Ok(comments);
        }

        // GET: api/comment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comments>> GetComment(int id)
        {
            var comment = await _commentDao.GetComment(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        // PUT: api/comment/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, Comments comment)
        {
            if (id != comment.Id)
            {
                return BadRequest();
            }

            try
            {
                await _commentDao.UpdateComment(comment);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
            {
                if (!_commentDao.CommentExists(id))
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

        // POST: api/comment
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Comments>> PostComment(Comments comment)
        {
            await _commentDao.AddComment(comment);
            return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, comment);
        }

        // DELETE: api/comment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _commentDao.GetComment(id);
            if (comment == null)
            {
                return NotFound();
            }

            var result = await _commentDao.DeleteComment(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
