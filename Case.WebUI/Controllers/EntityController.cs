using AutoMapper;
using Case.Business.Abstracts;
using Case.Models;
using Case.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using X.PagedList;

namespace Case.WebUI.Controllers
{
    public class EntityController : Controller
    {
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;
        private readonly ILogger<EntityController> _logger;
        private readonly string _webApiBaseUrl = "https://localhost:7032/api/Person";


        public EntityController(IPersonService personService, IMapper mapper, ILogger<EntityController> logger)
        {
            _personService = personService;
            _mapper = mapper;
            _logger = logger;
        }

        /*
        public IActionResult Index(int? page, string searchTerm)
        {

            int pageNumber = page ?? 1;
            int pageSize = 1;

            var peopleQuery = _personService.GetList();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                peopleQuery = peopleQuery.Where(p => p.Name.Contains(searchTerm)).ToList();
                var searchPerson=peopleQuery.ToPagedList(pageNumber, pageSize);
                return View(searchPerson);
            }

            var pagedPerson = peopleQuery.ToPagedList(pageNumber, pageSize);

            return View(pagedPerson);
        }
        */

        public IActionResult Index(int? page, string? searchTerm)
        {
            int pageNumber = page ?? 1;
            int pageSize = 5;

            var people = GetPagedPeopleFromApi(pageNumber, pageSize, searchTerm).Result;

            return View(people);
        }

        private async Task<IPagedList<Person>> GetPagedPeopleFromApi(int pageNumber, int pageSize, string? searchTerm)
        {
            using (var client = new HttpClient())
            {
                string requestUri = $"{_webApiBaseUrl}?page={pageNumber}";
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    requestUri += $"&searchTerm={searchTerm}";
                }

                var response = await client.GetAsync(requestUri);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var persons = JsonSerializer.Deserialize<List<Person>>(content, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    var result = new PagedList<Person>(persons, pageNumber, pageSize);
                    return result;
                }
                else
                {
                    // Handle error here if needed
                    return new PagedList<Person>(new List<Person>(), pageNumber, pageSize);
                }
            }
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(PersonCreateDTO personDTO)
        {
            if (ModelState.IsValid)
            {
                // var person = _mapper.Map<Person>(personDTO);

                await AddPersonToApiAsync(personDTO);

                return RedirectToAction("Index");
            }

            return View(personDTO);
        }

        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string name)
        {
            await DeletePersonFromApiAsync(name);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await DeletePersonFromApiAsync(id.ToString());
            return RedirectToAction("Index");
        }

        private async Task AddPersonToApiAsync(PersonCreateDTO personDTO)
        {
            var json = JsonSerializer.Serialize(personDTO);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync($"{_webApiBaseUrl}", content);

                if (!response.IsSuccessStatusCode)
                {
                }
            }
        }

        private async Task DeletePersonFromApiAsync(string parameter)
        {
            using (var client = new HttpClient())
            {
                var response = await client.DeleteAsync($"{_webApiBaseUrl}/Delete/{parameter}");

                if (!response.IsSuccessStatusCode)
                {
                }
            }
        }
        /*

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(PersonCreateDTO personDTO)
        {
            var person = _mapper.Map<Person>(personDTO);
            _personService.Add(person);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete()
        {
            // Handle Record Delete logic here
            return View();
        }

        [HttpPost]
        public IActionResult Delete(string name)
        {
            var person=_personService.GetPersonByName(name);
            if (person != null)
            {
                _personService.Remove(person);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var person = _personService.GetByID(id);

            if (person == null)
            {
                return NotFound();
            }
            _personService.Remove(person);

            return RedirectToAction("Index");
        }
        */
    }
}
