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

        public DbSet<TextLog> TextLog { get; set; }
        public DbSet<ImageLog> ImageLog { get; set; }
    }
}