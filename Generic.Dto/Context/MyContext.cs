using Microsoft.EntityFrameworkCore;

namespace Generic.Dto.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }
    }
}