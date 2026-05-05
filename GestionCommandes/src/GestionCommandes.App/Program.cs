using GestionCommandes.App.Data;
using GestionCommandes.App.Forms;
using GestionCommandes.App.Repositories;
using GestionCommandes.App.Services;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace GestionCommandes.App;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();

        var services = new ServiceCollection();
        ConfigureServices(services);

        using ServiceProvider provider = services.BuildServiceProvider();
        InitializeDatabase(provider);

        Application.Run(provider.GetRequiredService<MainForm>());
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>();
        services.AddTransient<ClientRepository>();
        services.AddTransient<ProductRepository>();
        services.AddTransient<CommandeRepository>();
        services.AddTransient<ClientService>();
        services.AddTransient<ProductService>();
        services.AddTransient<CommandeService>();
        services.AddTransient<FormClient>();
        services.AddTransient<FormProduit>();
        services.AddTransient<FormCommande>();
        services.AddTransient<MainForm>();
    }

    private static void InitializeDatabase(IServiceProvider provider)
    {
        using IServiceScope scope = provider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.EnsureCreated();
    }
}
