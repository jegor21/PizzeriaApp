using PizzeriaApp;
using PizzeriaApp.Services;

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
            });

        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "pizzeria.db3");
        builder.Services.AddSingleton(s => new DatabaseService(dbPath));

        return builder.Build();
    }
}
