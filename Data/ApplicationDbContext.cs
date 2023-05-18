using Microsoft.EntityFrameworkCore;
using OPENAI.Models;

namespace OPENAI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<ChatLog> ChatLog { get; set; }
    }
}
