using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace PeopleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SomeController : ControllerBase
    {
        [HttpGet("sync")]
        public IActionResult GetSync()
        {
            Stopwatch stop = Stopwatch.StartNew();
            stop.Start();

            Thread.Sleep(1000);
            Console.WriteLine("Conexion a la bd");
            Thread.Sleep(1000);
            Console.WriteLine("Terminado correo");

            Console.WriteLine("todo ha terminado");
            stop.Stop();
            return Ok(stop.Elapsed);
        }

        [HttpGet("async")]
        public async Task<IActionResult> GetAsync()
        {
            Stopwatch stop = Stopwatch.StartNew();
            stop.Start();

            var task1 = new Task<int>(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("Conexion a la bd");
                return 1;
            });

            var task2 = new Task<int>(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("Terminado correo");
                return 2;
            });

            task1.Start();
            task2.Start();

            Console.WriteLine("hago otra cosa");

            var result1 = await task1;
            var result2 = await task2;

            Console.WriteLine("Todo acabo");

            stop.Stop();

            return Ok(result1 + " "+ result2 + "" + stop.Elapsed);
        }
    }
}
