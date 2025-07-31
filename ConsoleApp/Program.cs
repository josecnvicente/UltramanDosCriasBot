using Business.Domain.Bot;
using Domain.DTO.Config;
using Domain.Interface.Business.Bot;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ConfigAppSettings();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var botBusiness = serviceProvider.GetService<IBotBusiness>();

            await botBusiness.RunBotAsync();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache()
                .AddSingleton<IBotBusiness, BotBusiness>()
                .AddSingleton<IChoicesBusiness, ChoicesBusiness>()
                .AddSingleton<ILolBusiness, LolBusiness>()
                .AddSingleton<IJokeBusiness, JokeBusiness>();
        }

        private static void ConfigAppSettings()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            IConfiguration config = builder.Build();

            ConfigDto.DiscordConfig = config.GetSection("Discord").Get<DiscordDto>();
        }
    }
}