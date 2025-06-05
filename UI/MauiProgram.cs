using Application.Interfaces;
using Application.UseCases;
using Domain.Interfaces;
using Infrastructure;
using Microsoft.Extensions.Logging;
using UI.Services;

namespace UI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Строка подключения к локальной MongoDB:
            string connectionString = "mongodb://localhost:27017";
            string dbName = "PoleChudes";

            builder.Services.AddSingleton<ISequenceGenerator>(
                sp => new MongoSequenceGenerator(connectionString, dbName)
            );

            builder.Services.AddSingleton<IUserRepository>(
                sp => new MongoUserRepository(
                    connectionString,
                    dbName,
                    sp.GetRequiredService<ISequenceGenerator>()
                )
            );

            builder.Services.AddSingleton<CurrentUserService>();
            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddTransient<GamePage>();

            builder.Services.AddSerialization();
            builder.Services.AddSingleton<IFileSystemPath, MauiFileSystem>();
            builder.Services.AddSingleton<ISaveRepository, SaveRepository>();
            builder.Services.AddSingleton<SaveService>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
