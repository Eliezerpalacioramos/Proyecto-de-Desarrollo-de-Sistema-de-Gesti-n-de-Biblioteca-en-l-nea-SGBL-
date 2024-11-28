using Microsoft.EntityFrameworkCore;
using Moq;
using SGBL.Business.Services;
using SGBL.Data.Contexts;
using SGBL.Data.Models;
using System.Data.Entity;
using System.Linq.Expressions;
using Xunit;
using Xunit.Sdk;

namespace SGBL.Test
{
    public class Tests
    {
        [Fact]
        public async Task RegisterUser_ShouldReturnTrue_WhenEmailIsNotTaken()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<LibraryDbContext>()
                .UseSqlServer("Server=YOUR_SERVER;Database=TestDatabase;Trusted_Connection=True;")
                .Options;

            using (var context = new LibraryDbContext(options))
            {
                context.Users.RemoveRange(context.Users);
                await context.SaveChangesAsync();

                var userService = new UserService(context);

                var newUser = new User
                {
                    Email = "newuser@example.com",
                    Password = "Password123"
                };

                // Act
                var result = await userService.RegisterUser(newUser);

                // Assert
                Assert.IsTrue(result);

                var savedUser = context.Users.FirstOrDefault(u => u.Email == "newuser@example.com");
                Assert.IsNotNull(savedUser);
                Assert.IsTrue(BCrypt.Net.BCrypt.Verify("Password123", savedUser.Password));
            }
        }


        [Fact]
        public async Task RegisterUser_ShouldReturnFalse_WhenEmailIsTaken()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<LibraryDbContext>()
                .UseSqlServer("Server=YOUR_SERVER;Database=TestDatabase;Trusted_Connection=True;")
                .Options;

            using (var context = new LibraryDbContext(options))
            {
                context.Users.RemoveRange(context.Users);
                await context.SaveChangesAsync();

                var existingUser = new User
                {
                    Email = "existinguser@example.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("Password123")
                };
                context.Users.Add(existingUser);
                await context.SaveChangesAsync();

                var userService = new UserService(context);

                var newUser = new User
                {
                    Email = "existinguser@example.com",
                    Password = "Password123"
                };

                // Act
                var result = await userService.RegisterUser(newUser);

                // Assert
                Assert.IsFalse(result);
            }
        }

        [Fact]
        public async Task RegisterUser_ShouldEncryptPassword_WhenUserIsRegistered()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<LibraryDbContext>()
                .UseSqlServer("Server=YOUR_SERVER;Database=TestDatabase;Trusted_Connection=True;")
                .Options;

            using (var context = new LibraryDbContext(options))
            {
                context.Users.RemoveRange(context.Users);
                await context.SaveChangesAsync();

                var userService = new UserService(context);

                var newUser = new User
                {
                    Email = "newuser@example.com",
                    Password = "Password123"
                };

                // Act
                var result = await userService.RegisterUser(newUser);

                // Assert
                Assert.IsTrue(result);
                var savedUser = context.Users.FirstOrDefault(u => u.Email == "newuser@example.com");
                Assert.IsNotNull(savedUser);
                Assert.IsTrue(BCrypt.Net.BCrypt.Verify("Password123", savedUser.Password));
            }
        }

        [Fact]
        public async Task IsEmailTaken_ShouldReturnTrue_WhenEmailIsRegistered()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<LibraryDbContext>()
                .UseSqlServer("Server=YOUR_SERVER;Database=TestDatabase;Trusted_Connection=True;")
                .Options;

            using (var context = new LibraryDbContext(options))
            {
                context.Users.RemoveRange(context.Users);
                await context.SaveChangesAsync();

                var existingUser = new User
                {
                    Email = "existinguser@example.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("Password123")
                };
                context.Users.Add(existingUser);
                await context.SaveChangesAsync();

                var userService = new UserService(context);

                // Act
                var result = await userService.IsEmailTaken("existinguser@example.com");

                // Assert
                Assert.IsTrue(result);
            }
        }

        [Fact]
        public async Task IsEmailTaken_ShouldReturnFalse_WhenEmailIsNotRegistered()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<LibraryDbContext>()
                .UseSqlServer("Server=YOUR_SERVER;Database=TestDatabase;Trusted_Connection=True;")
                .Options;

            using (var context = new LibraryDbContext(options))
            {
                context.Users.RemoveRange(context.Users);
                await context.SaveChangesAsync();

                var userService = new UserService(context);

                // Act
                var result = await userService.IsEmailTaken("newuser@example.com");

                // Assert
                Assert.IsFalse(result);
            }
        }
    }
}