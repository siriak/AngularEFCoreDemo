using AngularEFCoreDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AngularEFCoreDemo.Controllers
{
    [Route("api/[controller]")]
    public class ReaderTicketsController : Controller
    {
        private readonly LibraryContext context;

        public ReaderTicketsController(LibraryContext context) => this.context = context;

        [HttpGet]
        public ActionResult<ICollection<ReaderTicket>> Get() => context.ReaderTickets.ToList();

        [HttpGet("{id}")]
        public ActionResult<ReaderTicket> Get(int id) => context.ReaderTickets.Find(id);

        [HttpPost("{id}")]
        public ActionResult PostAsync(int id, [FromBody]ReaderTicket readerTicket)
        {
            if (id != readerTicket.ReaderTicketId)
            {
                return BadRequest();
            }

            try
            {
                context.ReaderTickets.Add(readerTicket);
                context.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost]
        public ActionResult PostAsync([FromBody]ReaderTicket[] readerTickets)
        {
            try
            {
                context.ReaderTickets.AddRange(readerTickets);
                context.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]ReaderTicket readerTicket)
        {
            if (id != readerTicket.ReaderTicketId)
            {
                return BadRequest();
            }

            try
            {
                context.ReaderTickets.Update(readerTicket);
                context.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut]
        public ActionResult Put([FromBody]ReaderTicket[] readerTickets)
        {
            try
            {
                context.ReaderTickets.UpdateRange(readerTickets);
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
