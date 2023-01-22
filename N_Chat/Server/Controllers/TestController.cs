using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using N_Chat.Shared;

namespace N_Chat.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        //Initialize the database making it accesible through context.[tableofchoice]
        private readonly DataContext context; 
        public TestController(DataContext context)
        {
            this.context = context;                             
        }
        //Initialize the database making it accesible through context.[tableofchoice]


        //Get method that takes all data in "Test" and puts it into a list asynchronicly
        [HttpGet("getall")]
        public async Task<IEnumerable<TestModel>> GetAll()
        {
            return await context.Test.OfType<TestModel>().ToListAsync();
        }
    }
}
