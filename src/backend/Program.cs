using stream_roulette.Configuration;
using stream_roulette.Persistence.Repositories;
using stream_roulette.Services.Donations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddHostedService<DonationsBackgroundService>();
builder.Services.Configure<StreamElementsOptions>(builder.Configuration.GetSection(StreamElementsOptions.Section));

builder.Services.AddSingleton<IDonationsRepository, DonationRepository>();

var app = builder.Build();


app.Run();
