using AngularEFCoreDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AngularEFCoreDemo.Controllers
{
    [Route("api/[controller]")]
    public class AuthorityEntriesController : Controller
    {
        private readonly LibraryContext context;

        public AuthorityEntriesController(LibraryContext context) => this.context = context;

        [HttpGet]
        public ActionResult<ICollection<AuthorityEntry>> Get() => context.AuthorityEntries.ToList();

        [HttpGet("{id}")]
        public ActionResult<AuthorityEntry> Get(int id) => context.AuthorityEntries.Find(id);

        [HttpPost("{id}")]
        public ActionResult PostAsync(int id, [FromBody]AuthorityEntry authorityEntry)
        {
            if (id != authorityEntry.AuthorityEntryId)
            {
                return BadRequest();
            }

            try
            {
                context.AuthorityEntries.Add(authorityEntry);
                context.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost]
        public ActionResult PostAsync([FromBody]AuthorityEntry[] authorityEntries)
        {
            try
            {
                context.AuthorityEntries.AddRange(authorityEntries);
                context.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]AuthorityEntry authorityEntry)
        {
            if (id != authorityEntry.AuthorityEntryId)
            {
                return BadRequest();
            }

            try
            {
                if (authorityEntry.IsDeleted)
                {
                    context.MarkDeleted(authorityEntry);
                }

                context.AuthorityEntries.Update(authorityEntry);
                context.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut]
        public ActionResult Put([FromBody]AuthorityEntry[] authorityEntries)
        {
            try
            {
                foreach (var authorityEntry in authorityEntries)
                {
                    if (authorityEntry.IsDeleted)
                    {
                        context.MarkDeleted(authorityEntry);
                    }
                }

                context.AuthorityEntries.UpdateRange(authorityEntries);
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
