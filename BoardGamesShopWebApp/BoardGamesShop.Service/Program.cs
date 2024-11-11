using BoardGamesShopWebApp.DI;
using BoardGamesShopWebApp.IoC;
using BoardGamesShopWebApp.Settings;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var settings = BoardGamesShopSettingsReader.Read(configuration);

var builder = WebApplication.CreateBuilder(args);

AppConfigurator.ConfigureServices(builder, settings);

var app = builder.Build();

AppConfigurator.ConfigureApplication(app);

app.Run();
