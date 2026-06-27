using Business.Domain.Bot;
using Domain.Business.Bot;
using Domain.DTO.Config;
using Domain.Interface.Business.Bot;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ConfigAppSettings();

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.SetMinimumLevel(LogLevel.Information);
            });
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var botBusiness = serviceProvider.GetService<IBotBusiness>()!;

            await botBusiness.RunBotAsync();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache()
                .AddSingleton<IBotBusiness, BotBusiness>()
                .AddSingleton<IChoicesBusiness, ChoicesBusiness>()
                .AddSingleton<ILolBusiness, LolBusiness>()
                .AddSingleton<IJokeBusiness, JokeBusiness>()
                .AddSingleton<ISharedBusiness, SharedBusiness>()
                .AddSingleton<IBirthdayBusiness, BirthdayBusiness>();
        }

        private static void ConfigAppSettings()
        {
            string? token = Environment.GetEnvironmentVariable("DISCORD_BOT_TOKEN");

            if (!string.IsNullOrEmpty(token))
            {
                ConfigDto.DiscordConfig = new DiscordDto { Token = token };
            }
            else
            {
                var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

                IConfiguration config = builder.Build();

                ConfigDto.DiscordConfig = config.GetSection("Discord").Get<DiscordDto>()!;
            }
        }
    }
}