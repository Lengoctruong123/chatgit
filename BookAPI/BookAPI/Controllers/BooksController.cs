using BookAPI.Models;
using BookAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAPI.Controllers
{
    [Route("pages/api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        [HttpGet]
        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _bookRepository.Get();
        }
        [HttpGet("{id}")]
        public async Task<Book> GetBook(int id)
        {
            return await _bookRepository.Get(id);
        }
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook( Book book)
        {
            var bookItem = new Book
            {
                Title = book.Title,
                Author = book.Author,
                Description = book.Description,            
            };
            var newbook = await _bookRepository.Create(bookItem);
            return CreatedAtAction(nameof(GetBooks), new { id = bookItem.ID }, bookItem);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> PutBooks(int id, [FromBody] Book book)
        {
            if (id != book.ID)
            {
                return BadRequest();
            }
            await _bookRepository.Update(book);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete (int id)
        {
            var bookToDelete = await _bookRepository.Get(id);
            if (bookToDelete == null)
            {
                return NotFound();
            }
            await _bookRepository.Delete(bookToDelete.ID);
            return NoContent();

        }   

        [HttpGet("search/{title}")]
        public async Task<IEnumerable<Book>> SearchBooks(string title)
        {
            return await _bookRepository.SearchBooks(title);
        }
        
        //public IQueryable<Book> Post(string title, string author)
        //{
        //   var book = _bookRepository.Login(title,author);
        //    return book;
        //}
    }
    
}
