using SGBL.Data.Contexts;

class Program
{
    static void Main(string[] args)
    {
        using (var context = new LibraryDbContext())
        {
            bool isConnected = context.TestConnection();
            if (isConnected)
            {
                Console.WriteLine("Conexión exitosa a la base de datos.");
            }
            else
            {
                Console.WriteLine("Error al conectar a la base de datos.");
            }
        }
    }
}