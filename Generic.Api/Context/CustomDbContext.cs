using Microsoft.EntityFrameworkCore;

namespace Generic.Api.Context
{
    public class CustomDbContext : DbContext
    {
        public CustomDbContext(DbContextOptions options) : base(options)
        {

        }
    }

}