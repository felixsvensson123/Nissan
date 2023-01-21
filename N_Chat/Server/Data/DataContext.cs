using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace N_Chat.Server.Data
{
    public class DataContext : IdentityDbContext<UserModel>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        //Följ detta sättet att namnge tables Testmodel = Test, ChatModel = Chat
        public DbSet<TestModel> Test { get; set; }
    }
}
