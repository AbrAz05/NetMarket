using BusinessLogic.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Creando Instancia de Host que va a ejecutar la aplicacion
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                // Instancia del ServiceProvider que permite ejecutar la migracion e instanciar al DbContext
                var services = scope.ServiceProvider;
                // Instanciar LoggerFactory para poder imprimir errores
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                try
                {
                    // Instanciando el DbContext
                    var context = services.GetRequiredService<MarketDbContext>();
                    // Iniciar proceso de Migracion
                    await context.Database.MigrateAsync();
                    // Instanciar la carga de la data al ejecutarse
                    await MarketDbContextData.CargarDataAsync(context, loggerFactory);
                }
                catch (Exception e)
                {
                    // Instancia de LoggerFacotry que me permite mostrar los problemas en caso de existir
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(e, "Errores en el proceso de Migracion");
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
