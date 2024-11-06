using SGBL.Data.Models;
using System.Data.Entity;

namespace SGBL.Data.Contexts
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext() : base("name=SGBLDbContext") { }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Loan> Loans { get; set; }

        public bool TestConnection()
        {
            try
            {
                this.Database.Connection.Open();
                this.Database.Connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar: {ex.Message}");
                return false;
            }
        }
    }
}

