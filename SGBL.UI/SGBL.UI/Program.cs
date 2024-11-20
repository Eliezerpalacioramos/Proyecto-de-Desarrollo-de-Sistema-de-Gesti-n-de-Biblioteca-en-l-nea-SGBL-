using SGBL.UI.Client.Pages;
using Microsoft.EntityFrameworkCore;
using SGBL.Business.Interfaces;
using SGBL.Data.Contexts;
using SGBL.Business.Services;
using SGBL.UI.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SGBLDbContext")));

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(SGBL.UI.Client._Imports).Assembly);

app.Run();
