using AngularEFCoreDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AngularEFCoreDemo.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private readonly LibraryContext context;

        public BooksController(LibraryContext context) => this.context = context;

        [HttpGet]
        public ActionResult<ICollection<Book>> Get() => context.Books.ToList();

        [HttpGet("{id}")]
        public ActionResult<Book> Get(int id) => context.Books.Find(id);

        [HttpPost("{id}")]
        public ActionResult PostAsync(int id, [FromBody]Book book)
        {
            if (id != book.BookId)
            {
                return BadRequest();
            }

            try
            {
                context.Books.Add(book);
                context.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost]
        public ActionResult PostAsync([FromBody]Book[] books)
        {
            try
            {
                context.Books.AddRange(books);
                context.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]Book book)
        {
            if (id != book.BookId)
            {
                return BadRequest();
            }

            try
            {
                context.Books.Update(book);
                context.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut]
        public ActionResult Put([FromBody]Book[] books)
        {
            try
            {
                context.Books.UpdateRange(books);
                context.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
