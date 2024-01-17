using AutoMapper;
using Case.Business.Abstracts;
using Case.Models;
using Case.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using X.PagedList;

namespace Case.WebUI.Controllers
{
    public class WebServiceController : Controller
    {
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;
        private readonly ILogger<WebServiceController> _logger;
        private readonly HttpClient _httpClient;

        public WebServiceController(IPersonService personService, IMapper mapper, ILogger<WebServiceController> logger, IHttpClientFactory httpClientFactory)
        {
            _personService = personService;
            _mapper = mapper;
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
            // Web servisinizin base adresini aşağıdaki gibi belirleyebilirsiniz.
            _httpClient.BaseAddress = new Uri("https://localhost:7087/Entity/");
        }

        public async Task<IActionResult> Index(int? page, string searchTerm)
        {
            int pageNumber = page ?? 1;
            int pageSize = 1;

            var peopleQuery = await GetPeopleFromWebServiceAsync();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                peopleQuery = peopleQuery.Where(p => p.Name.Contains(searchTerm)).ToList();
                var searchPerson = peopleQuery.ToPagedList(pageNumber, pageSize);
                return View(searchPerson);
            }

            var pagedPerson = peopleQuery.ToPagedList(pageNumber, pageSize);

            return View(pagedPerson);
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
                var person = _mapper.Map<Person>(personDTO);
                await AddPersonToWebServiceAsync(personDTO);

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
            await DeletePersonFromWebServiceAsync(name);
            return RedirectToAction("Index");
        }

        private async Task<IEnumerable<Person>> GetPeopleFromWebServiceAsync()
        {
            var response = await _httpClient.GetAsync("people");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var people = JsonSerializer.Deserialize<IEnumerable<Person>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return people;
            }

            // Hata durumunda bir şeyler yapabilirsiniz (loglama, hata sayfasına yönlendirme vb.)
            return Array.Empty<Person>();
        }

        private async Task AddPersonToWebServiceAsync(PersonCreateDTO personDTO)
        {
            var json = JsonSerializer.Serialize(personDTO);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("people", content);

            if (!response.IsSuccessStatusCode)
            {
                // Hata durumunda bir şeyler yapabilirsiniz (loglama, hata sayfasına yönlendirme vb.)
            }
        }

        private async Task DeletePersonFromWebServiceAsync(string name)
        {
            var response = await _httpClient.DeleteAsync($"people/{name}");

            if (!response.IsSuccessStatusCode)
            {
                // Hata durumunda bir şeyler yapabilirsiniz (loglama, hata sayfasına yönlendirme vb.)
            }
        }
    }
}
