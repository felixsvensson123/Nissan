/*using Microsoft.AspNetCore.Http;
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

        //Vi kan kalla "context" för databasen. använd context för att nå olika tables i vår databas sedan finns det massa metoder du kan använda dig för att göra olika typer av queries till databasen.
        //Fråga om hjälp med syntax för queries om du inte känner till någon 
        // documentation https://www.entityframeworktutorial.net/efcore/querying-in-ef-core.aspx

        //Get method that takes all data in "Test" and puts it into a list asynchronicly
        [HttpGet("getall")]
        public async Task<IEnumerable<TestModel>> GetAll()
        {
            return await context.Test.OfType<TestModel>().ToListAsync();
        }
        //Simple add method for rest api, takes in testmodel uses context.Addasync to post data
        [HttpPost("add")]
        public async Task<IActionResult> AddTest(TestModel testModel)
        {
            if(ModelState.IsValid)
            {
                 await context.AddAsync(new TestModel
                {
                    Id = testModel.Id,
                    Name = testModel.Name,
                    Description = testModel.Description,
                });
                await context.SaveChangesAsync();
            }
            return BadRequest(ModelState);
        }
        [HttpGet("get/{id}")]
        public async Task<TestModel> GetById(int id)
        {           
            return context.Test.SingleOrDefault(x => x.Id == id);
        }
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteTest(int id)
        {
            TestModel item = await GetById(id);
            context.Remove(item);
            await context.SaveChangesAsync();
            return Ok();
        }
           



    }
}
*/