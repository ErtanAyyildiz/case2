using AutoMapper;
using Case.Business.Abstracts;
using Case.Models.DTOs;
using Case.Models;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Case.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;
        private readonly ILogger<PersonController> _logger;

        public PersonController(IPersonService personService, IMapper mapper, ILogger<PersonController> logger)
        {
            _personService = personService;
            _mapper = mapper;
            _logger = logger;
        }
        /*
        [HttpGet]
        public IActionResult Get(int? page, string? searchTerm)
        {
            int pageNumber = page ?? 1;
            int pageSize = 1;

            var peopleQuery = _personService.GetList();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                peopleQuery = peopleQuery.Where(p => p.Name.Contains(searchTerm)).ToList();
                var searchPerson = peopleQuery.ToPagedList(pageNumber, pageSize);
                return Ok(searchPerson);
            }

            var pagedPerson = peopleQuery.ToPagedList(pageNumber, pageSize);

            return Ok(pagedPerson);
        }
        */

        [HttpGet]
        public IActionResult Get(int? pageN, int? pageS, string? searchTerm)
        {
            int pageNumber = pageN ?? 1;
            int pageSize = pageS ?? 200;

            var peopleQuery = _personService.GetList();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                peopleQuery = peopleQuery.Where(p => p.Name.Contains(searchTerm)).ToList();
            }

            var pagedPerson = new PagedList<Person>(peopleQuery, pageNumber, pageSize);

            return Ok(pagedPerson);
        }


        [HttpPost]
        public IActionResult Add([FromBody] PersonCreateDTO personDTO)
        {
            if (personDTO == null)
            {
                return BadRequest("Invalid input");
            }

            var person = _mapper.Map<Person>(personDTO);
            _personService.Add(person);

            return Ok("Person added successfully");
        }

        [HttpDelete]
        public IActionResult Delete(string name)
        {
            var person = _personService.GetPersonByName(name);

            if (person != null)
            {
                _personService.Remove(person);
                return Ok("Person deleted successfully");
            }

            return NotFound("Person not found");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var person = _personService.GetByID(id);

            if (person == null)
            {
                return NotFound("Person not found");
            }

            _personService.Remove(person);

            return Ok("Person deleted successfully");
        }
    }
}
