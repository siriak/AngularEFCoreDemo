using AngularEFCoreDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AngularEFCoreDemo.Controllers
{
    [Route("api/[controller]")]
    public class SectionsController : Controller
    {
        private readonly LibraryContext context;

        public SectionsController(LibraryContext context) => this.context = context;

        [HttpGet]
        public ActionResult<ICollection<Section>> Get() => context.Sections.ToList();

        [HttpGet("{id}")]
        public ActionResult<Section> Get(int id) => context.Sections.Find(id);

        [HttpPost("{id}")]
        public ActionResult PostAsync(int id, [FromBody]Section section)
        {
            if (id != section.SectionId)
            {
                return BadRequest();
            }

            try
            {
                context.Sections.Add(section);
                context.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost]
        public ActionResult PostAsync([FromBody]Section[] sections)
        {
            try
            {
                context.Sections.AddRange(sections);
                context.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]Section section)
        {
            if (id != section.SectionId)
            {
                return BadRequest();
            }

            try
            {
                context.Sections.Update(section);
                context.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut]
        public ActionResult Put([FromBody]Section[] sections)
        {
            try
            {
                context.Sections.UpdateRange(sections);
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
