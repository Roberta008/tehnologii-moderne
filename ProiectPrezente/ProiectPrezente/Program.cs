using Microsoft.EntityFrameworkCore;
using ProiectPrezente.Database;

// Crează o aplicație web folosind WebApplicationBuilder
WebApplicationBuilder webAppBuilder = WebApplication.CreateBuilder(args: args);

// Accesează colecția de servicii
IServiceCollection appServices = webAppBuilder.Services;

// Adaugă controlere și vederi MVC
appServices.AddControllersWithViews();

// Adaugă pagini Razor cu compilare la runtime pentru dezvoltare
appServices.AddRazorPages().AddRazorRuntimeCompilation();

// Adaugă DbContext pentru Entity Framework Core cu conexiune SQL Server
appServices.AddDbContext<DatabaseContext>
(
    optionsAction: contextOptions =>
    {
        // Obține șirul de conexiune din configurare
        string? connectionString = webAppBuilder.Configuration.GetConnectionString(name: "DefaultConnection");
        // Configurează DbContext pentru a utiliza SQL Server
        contextOptions.UseSqlServer(connectionString: connectionString);
    }
);

// Adaugă cache distribuit în memorie pentru stocarea sesiunilor
appServices.AddDistributedMemoryCache();

// Adaugă serviciul pentru a accesa contextul HTTP în cadrul aplicației
appServices.AddHttpContextAccessor();

// Adaugă sesiuni cu un timeout de inactivitate de 30 de minute
appServices.AddSession(sessionOptions =>
{
    sessionOptions.IdleTimeout = TimeSpan.FromMinutes(30);
});

// Construiește aplicația web
WebApplication webApp = webAppBuilder.Build();

// Configurează calea de gestionare a erorilor
if (!webApp.Environment.IsDevelopment())
{
    // Folosește un gestionar global de excepții în medii non-dezvoltare
    webApp.UseExceptionHandler(errorHandlingPath: "/Home/Error");
}

// Activează furnizarea de fișiere statice (de ex., CSS, imagini)
webApp.UseStaticFiles();

// Activează rutarea
webApp.UseRouting();

// Activează autorizarea
webApp.UseAuthorization();

// Activează sesiunile
webApp.UseSession();

// Mapare ruta implicită pentru controale
webApp.MapControllerRoute
(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

// Rulează aplicația
webApp.Run();
