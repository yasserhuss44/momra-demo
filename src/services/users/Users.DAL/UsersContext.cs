using Microsoft.EntityFrameworkCore;


namespace Users.DAL
{
    public class UsersContext:DbContext
    {
        public UsersContext(DbContextOptions<UsersContext> dbContextOptions)
     : base(dbContextOptions)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
