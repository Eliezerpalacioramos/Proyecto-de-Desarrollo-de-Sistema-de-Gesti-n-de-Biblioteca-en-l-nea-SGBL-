using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SGBL.Data.Contexts;

public class AuthService 
{
    private readonly LibraryDbContext _dbContext;

    // Inyección de dependencias del DbContext
    public AuthService(LibraryDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<bool> LoginAsync(string email, string password)
    {
        try
        {
            // Busca el usuario en la base de datos
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                // Usuario no encontrado
                Console.WriteLine("Usuario no encontrado.");
                return false;
            }

            // Verifica si la contraseña coincide
            if (user.Password == password)
            {
                // Login exitoso
                Console.WriteLine($"Usuario autenticado: {user.Name}");
                return true;
            }
            else
            {
                // Contraseña incorrecta
                Console.WriteLine("Contraseña incorrecta.");
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error durante la autenticación: {ex.Message}");
            return false;
        }
    }
}


