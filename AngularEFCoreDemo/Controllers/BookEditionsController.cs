using AngularEFCoreDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AngularEFCoreDemo.Controllers
{
    [Route("api/[controller]")]
    public class BookEditionsController : Controller
    {
        private readonly LibraryContext context;

        public BookEditionsController(LibraryContext context) => this.context = context;

        [HttpGet]
        public ActionResult<ICollection<BookEdition>> Get() => context.BookEditions.ToList();

        [HttpGet("{id}")]
        public ActionResult<BookEdition> Get(int id) => context.BookEditions.Find(id);

        [HttpPost("{id}")]
        public ActionResult PostAsync(int id, [FromBody]BookEdition bookEdition)
        {
            if (id != bookEdition.BookEditionId)
            {
                return BadRequest();
            }

            try
            {
                context.BookEditions.Add(bookEdition);
                context.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost]
        public ActionResult PostAsync([FromBody]BookEdition[] bookEditions)
        {
            try
            {
                context.BookEditions.AddRange(bookEditions);
                context.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]BookEdition bookEdition)
        {
            if (id != bookEdition.BookEditionId)
            {
                return BadRequest();
            }

            try
            {
                if (bookEdition.IsDeleted)
                {
                    context.MarkDeleted(bookEdition);
                }

                context.BookEditions.Update(bookEdition);
                context.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut]
        public ActionResult Put([FromBody]BookEdition[] bookEditions)
        {
            try
            {
                foreach (var bookEdition in bookEditions)
                {
                    if (bookEdition.IsDeleted)
                    {
                        context.MarkDeleted(bookEdition);
                    }
                }

                context.BookEditions.UpdateRange(bookEditions);
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
