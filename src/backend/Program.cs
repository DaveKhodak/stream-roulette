using stream_roulette.Configuration;
using stream_roulette.DI;
using stream_roulette.Services.Donations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddHostedService<DonationsBackgroundService>();
builder.Services.Configure<StreamElementsOptions>(builder.Configuration.GetSection(StreamElementsOptions.Section));

InfrastructureDI.Configure(builder.Services, builder.Configuration);

var app = builder.Build();


app.Run();
