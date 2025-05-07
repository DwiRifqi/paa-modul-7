using Microsoft.AspNetCore.Mvc;
using paa_modul_7.Data;
using paa_modul_7.Models;

namespace paa_modul_7.Controllers
{
    [ApiController]
    [Route("api/person")]
    public class PersonController : ControllerBase
    {
        private readonly string _constr;

        public PersonController(IConfiguration configuration)
        {
            _constr = configuration.GetConnectionString("WebApiDatabase");
        }

        // GET: api/person
        [HttpGet]
        public ActionResult<List<Person>> GetAllPersons()
        {
            var context = new PersonContext(_constr);
            var people = context.ListPerson();
            return Ok(people);
        }

        // GET: api/person/{id}
        [HttpGet("{id}")]
        public ActionResult<Person> GetPersonById(int id)
        {
            var context = new PersonContext(_constr);
            var person = context.getPersonById(id);

            if (person == null)
                return NotFound(new { message = "Person not found" });

            Console.WriteLine($"Person found: {person.Id}, {person.Name}, {person.Age}");
            return Ok(person);
        }


        // GET: api/person/detail/{id}
        [HttpGet("detail/{id}")]
        public ActionResult<PersonDetail> GetPersonDetail(int id)
        {
            var context = new DetailContext(_constr);
            var detail = context.getPersonDetail(id);
            if (detail == null)
                return NotFound(new { message = "Person detail not found" });

            return Ok(detail);
        }
    }
}




/*using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using paa_modul_7.Models;
using System.Xml.Linq;

namespace paa_modul_7.Controllers
{
    public class PersonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet(Name = "Get All User")]
        public IEnumerable<Person> Get()
        {
            return Person.People;
        }

        [HttpGet("id", Name = "Get User By Id")]
        public ActionResult Get(int id)
        {
            Person? foundPerson = Person.People.FirstOrDefault(x => x.Id == id);
            if (foundPerson == null)
            {
                return NotFound();
            }
            return Ok(foundPerson);
        }

        [HttpGet("detail/{id}", Name = "Get User Detail By Id")]
        public async Task<ActionResult> GetDetail(int id)
        {
            Person? foundPerson = Person.People.FirstOrDefault(x => x.Id == id);
            if (foundPerson == null)
            {
                return NotFound();
            }

            using (HttpClient client = new HttpClient())
            {
                string url = $"https://dummy-user-tan.vercel.app/user";
                HttpResponseMessage response = await client.GetAsync(url);
                PersonDetail personDetail;
                if (response.IsSuccessStatusCode)
                {
                    PersonDetailFromAPI? personDetailFromAPI = await response
                        .Content.ReadFromJsonAsync<PersonDetailFromAPI>();
                    if (personDetailFromAPI != null)
                    {
                        personDetail = new PersonDetail
                        {
                            Id = foundPerson.Id,
                            Name = foundPerson.Name,
                            Age = foundPerson.Age,
                            Detail = new Detail
                            {
                                Saldo = personDetailFromAPI.Saldo,
                                Hutang = personDetailFromAPI.Hutang
                            }
                        };
                        return Ok(value: personDetail);
                    }
                }

                personDetail = new PersonDetail
                {
                    Id = foundPerson.Id,
                    Name = foundPerson.Name,
                    Age = foundPerson.Age,
                    Detail = null
                };
                return Ok(value: personDetail);
            }
        }
    }
}

*/