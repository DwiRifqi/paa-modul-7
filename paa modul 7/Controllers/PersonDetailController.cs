using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using paa_modul_7.Data;
using paa_modul_7.Models;

namespace paa_modul_7.Controllers
{
    public class PersonDetailController : ControllerBase
    {
        private readonly string _constr;
        private readonly HttpClient _httpClient;
        public PersonDetailController(IConfiguration configuration)
        {
            _constr = configuration.GetConnectionString("WebApiDatabase");
            _httpClient = new HttpClient();
        }

        [HttpGet("import-data")]
        public async Task<IActionResult> ImportPersonDetails()
        {
            string apiUrl = "https://dummy-user-tan.vercel.app/user";
            List<PersonDetailFromAPI> personDetailsFromApi;

            try
            {
                var response = await _httpClient.GetStringAsync(apiUrl);
                personDetailsFromApi = JsonConvert.DeserializeObject<List<PersonDetailFromAPI>>(response);
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving data from API: {ex.Message}");
            }

            try
            {
                DetailContext context = new DetailContext(_constr);
                context.InsertPersonDetails(personDetailsFromApi);
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Error inserting data into database: {ex.Message}");
            }
            return Ok("Data Anda Berhasil Ditambahkan!");
        }
    }
}
