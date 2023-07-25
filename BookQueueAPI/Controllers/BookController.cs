using BookQueue.Domain.Interfaces;
using BookQueue.Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BookQueue.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookManager _bookManager;

        public BookController(IBookManager bookManager)
        {
            _bookManager = bookManager;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bookManager.GetAllBooks());
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(_bookManager.GetBookById(id));
        }

        [HttpPost]
        public IActionResult Post(BookModel book)
        {
            if (ModelState.IsValid)
            {
                _bookManager.SaveBook(book);
                return Ok();
            }
            else
            {
                string messages = string.Join("\n", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));

                return BadRequest(messages);
            }

        }

        [HttpPatch("{id}")]
        public IActionResult Patch([FromRoute] Guid id, [FromBody] JsonPatchDocument<BookModel> bookPatch)
        {
            var book = _bookManager.GetBookById(id);

            if (book != null)
            {
                bookPatch.ApplyTo(book);
                _bookManager.SaveBook(book);
                return Ok();
            }
            else
                return NotFound();

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _bookManager.DeleteBook(id);
            return Ok();
        }
    }
}
