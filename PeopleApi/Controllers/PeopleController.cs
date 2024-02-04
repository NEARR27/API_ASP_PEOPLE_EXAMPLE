using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeopleApi.Services;

namespace PeopleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private IPeopleService _peopleService;

        public PeopleController([FromKeyedServices("peopleServices")]IPeopleService peopleService)
        {
            _peopleService =  peopleService;
        }

        [HttpGet("all")]
        public List<People> GetPeople() => Repostitory.People;

        [HttpGet("{id}")]
        public ActionResult<People> Get(int id)
        {
            var people = Repostitory.People.FirstOrDefault(p => p.Id == id);
            if ( people == null)
            {   
                return NotFound();

            }

            return Ok(people);
        }
        [HttpGet("search/{search}")]
        public List<People> Get(string search) => 
            Repostitory.People.Where(p => p.Name.ToUpper().Contains(search.ToUpper())).ToList();

        [HttpPost]
        public IActionResult Add(People people)
        {
            if(!_peopleService.Validate(people))
            {
                return BadRequest();
            }

            Repostitory.People.Add(people);

            return NoContent();
        }
    }

    public class Repostitory
    {
        public static List<People> People = new List<People>
        {
            new People()
            {
                Id = 1,Name="Pedro", BirthDate=new DateTime(1990,12,3)

            },
            new People()
            {
                Id = 2,Name="Aquino", BirthDate=new DateTime(1980,12,3)
            },
            new People()
            {
                Id = 3,Name="Julian", BirthDate=new DateTime(1990,12,3)
            },
            new People()
            {
                Id = 4,Name="Juan", BirthDate=new DateTime(1990,12,3)
            }
        };
    }

    public class People
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
