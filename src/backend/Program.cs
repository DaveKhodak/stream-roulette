using stream_roulette.Configuration;
using stream_roulette.Services.Donations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<StreamElementsOptions>(builder.Configuration.GetSection(StreamElementsOptions.Section));

builder.Services.AddSignalR();
builder.Services.AddHostedService<DonationsBackgroundService>();

var app = builder.Build();


app.Run();
