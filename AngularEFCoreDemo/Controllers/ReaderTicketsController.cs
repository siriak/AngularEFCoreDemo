using AngularEFCoreDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public ActionResult<ICollection<ReaderTicket>> Get() => context.ReaderTickets.Include(t => t.Reader).ToList();

        [HttpGet("{id}")]
        public ActionResult<ReaderTicket> Get(int id)
        {
            var ticket = context.ReaderTickets.Find(id);
            ticket.Reader = context.People.Find(ticket.ReaderId);
            return ticket;
        }

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
                readerTicket.Reader = null;
                if (readerTicket.IsDeleted)
                {
                    context.MarkDeleted(readerTicket);
                }

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
                foreach (var readerTicket in readerTickets)
                {
                    readerTicket.Reader = null;
                    if (readerTicket.IsDeleted)
                    {
                        context.MarkDeleted(readerTicket);
                    }
                }

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
