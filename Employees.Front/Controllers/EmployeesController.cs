using Employees.Front.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Employees.Front.Controllers
{
    public class EmployeesController : Controller
    {

        private readonly IHttpClientFactory _httpClient;

        public EmployeesController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        // GET: EmployeesController
        public async Task<ActionResult> Index()
        {
            List<EmployeesDTO> employees = new List<EmployeesDTO>();
            try
            {
                var client = _httpClient.CreateClient("EmployeesApi");
                var response = await client.GetAsync("Employees");
                if (response.IsSuccessStatusCode)
                {
                    var content = await  response.Content.ReadAsStringAsync();

                    var result =  JsonSerializer.Deserialize<GenericResponse>(content, options);
                    if(result.HttpCode == System.Net.HttpStatusCode.OK)
                        employees = JsonSerializer.Deserialize<List<EmployeesDTO>>(result.Data.ToString(), options);


                    return View(employees);
                }
                return View();

            }
            catch 
            {
                return View("Error");
            }
        }


        // GET: EmployeesController/Create
        public  ActionResult Create()
        {

            return View();

        }

        // POST: EmployeesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeRequest employee)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var client = _httpClient.CreateClient("EmployeesApi");
                    var response = await client.PostAsJsonAsync("Employees", employee);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();

                        var result = JsonSerializer.Deserialize<GenericResponse>(content, options);
                        if (result.HttpCode == System.Net.HttpStatusCode.OK)
                            return RedirectToAction(nameof(Index));
                    }
                }
                return View();
            }
            catch
            {
                return View("Error");
            }
        }

    }
}
