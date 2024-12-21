using LibraryCore;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi
{
    public class Upravlenie_bibliotekoyEntities : DbContext
    {
        public Upravlenie_bibliotekoyEntities(DbContextOptions<Upravlenie_bibliotekoyEntities> options) : base(options)
        {
            // Инициализация свойств DbSet
            Authors = Set<Authors>();
            Books = Set<Books>();
            Readers = Set<Readers>();
            Loans = Set<Loans>();
        }

        public DbSet<Authors> Authors { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<Readers> Readers { get; set; }
        public DbSet<Loans> Loans { get; set; }
    }
}

