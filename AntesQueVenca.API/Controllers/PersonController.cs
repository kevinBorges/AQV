using System.Collections.Generic;
using System.Linq;
using AntesQueVenca.Data.Context;
using AntesQueVenca.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AntesQueVenca.API.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        AntesQueVencaContext _aqvContext = new AntesQueVencaContext();

      
        [HttpPost]
        [AllowAnonymous]
        public void PostPerson(Person person)
        {
            _aqvContext.Person.Add(person);
            _aqvContext.SaveChanges();
        }

        [HttpPut]
        [AllowAnonymous]
        public void UpdatePerson(Person person)
        {
            _aqvContext.Person.Update(person);
            _aqvContext.SaveChanges();
        }

        [HttpGet]
        [AllowAnonymous]
        public Person GetPerson(int id)
        {
            var selectedPerson = _aqvContext.Person.Find(id);

            return selectedPerson;
        }

        [HttpGet]
        [AllowAnonymous]
        public List<Person> GetAllPerson()
        {
            var personList = _aqvContext.Person.Where(p => !p.Deleted).ToList();

            return personList;
        }

        [HttpDelete]
        [AllowAnonymous]
        public void DeletePerson(int id)
        {
            var selectedPerson = _aqvContext.Person.Find(id);
            _aqvContext.Person.Remove(selectedPerson);
            _aqvContext.SaveChanges();
        }

    }
}
