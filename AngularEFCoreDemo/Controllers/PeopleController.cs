using AngularEFCoreDemo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AngularEFCoreDemo.Controllers
{
    [Route("api/[controller]")]
    public class PeopleController : Controller
    {
        private readonly LibraryContext context;

        public PeopleController(LibraryContext context) => this.context = context;

        [HttpGet]
        public ActionResult<ICollection<Person>> Get() => context.People.ToList();

        [HttpGet("{id}")]
        public ActionResult<Person> Get(int id) => context.People.Find(id);

        [HttpPost("{id}")]
        public ActionResult PostAsync(int id, [FromBody]Person person)
        {
            if (id != person.PersonId)
            {
                return BadRequest();
            }

            try
            {
                context.People.Add(person);
                context.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost]
        public ActionResult PostAsync([FromBody]Person[] people)
        {
            try
            {
                context.People.AddRange(people);
                context.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]Person person)
        {
            if (id != person.PersonId)
            {
                return BadRequest();
            }

            try
            {
                if (person.IsDeleted)
                {
                    context.MarkDeleted(person);
                }

                context.People.Update(person);
                context.SaveChanges();
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut]
        public ActionResult Put([FromBody]Person[] people)
        {
            try
            {
                foreach (var person in people)
                {
                    if (person.IsDeleted)
                    {
                        context.MarkDeleted(person);
                    }
                }

                context.People.UpdateRange(people);
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
