using ProiectPrezentaOnline.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder webAppBuilder = WebApplication.CreateBuilder(args: args);

// Add services to the container.
IServiceCollection serviceCollection = webAppBuilder.Services;
serviceCollection.AddControllersWithViews();
serviceCollection.AddRazorPages().AddRazorRuntimeCompilation();
serviceCollection.AddDbContext<DatabaseContext>
(
    optionsAction: contextOptions =>
    {
        contextOptions.UseSqlServer(connectionString: webAppBuilder.Configuration.GetConnectionString(name: "DefaultConnection"));
    }
);
serviceCollection.AddHttpContextAccessor();
serviceCollection.AddDistributedMemoryCache();
serviceCollection.AddSession
(
    configure: sessionOptions =>
    {
        sessionOptions.IdleTimeout = TimeSpan.FromMinutes(value: 30);
        sessionOptions.Cookie.HttpOnly = true;
        sessionOptions.Cookie.IsEssential = true;
    }
);

WebApplication webApp = webAppBuilder.Build();

// Configure the HTTP request pipeline.
if (!webApp.Environment.IsDevelopment())
{
    webApp.UseExceptionHandler(errorHandlingPath: "/Home/Error");
    webApp.UseHsts();
}

webApp.UseHttpsRedirection();
webApp.UseStaticFiles();

webApp.UseRouting();

webApp.UseAuthorization();

webApp.UseSession();

webApp.MapControllerRoute
(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

webApp.Run();
