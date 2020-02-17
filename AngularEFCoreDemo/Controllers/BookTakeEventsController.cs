using AngularEFCoreDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AngularEFCoreDemo.Controllers
{
    [Route("api/[controller]")]
    public class BookTakeEventsController : Controller
    {
        private readonly LibraryContext context;

        public BookTakeEventsController(LibraryContext context) => this.context = context;

        [HttpGet]
        public ActionResult<ICollection<BookTakeEvent>> Get() => context.BookTakeEvents.ToList();

        [HttpGet("{id}")]
        public ActionResult<BookTakeEvent> Get(int id) => context.BookTakeEvents.Find(id);

        [HttpPost("{id}")]
        public ActionResult PostAsync(int id, [FromBody]BookTakeEvent bookTakeEvent)
        {
            if (id != bookTakeEvent.BookTakeEventId)
            {
                return BadRequest();
            }

            try
            {
                context.BookTakeEvents.Add(bookTakeEvent);
                context.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost]
        public ActionResult PostAsync([FromBody]BookTakeEvent[] bookTakeEvents)
        {
            try
            {
                context.BookTakeEvents.AddRange(bookTakeEvents);
                context.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]BookTakeEvent bookTakeEvent)
        {
            if (id != bookTakeEvent.BookTakeEventId)
            {
                return BadRequest();
            }

            try
            {
                context.BookTakeEvents.Update(bookTakeEvent);
                context.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut]
        public ActionResult Put([FromBody]BookTakeEvent[] bookTakeEvents)
        {
            try
            {
                context.BookTakeEvents.UpdateRange(bookTakeEvents);
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
