using stream_roulette.Configuration;
using stream_roulette.DI;
using stream_roulette.Services.Donations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddSignalR();
builder.Services.AddHostedService<DonationsBackgroundService>();
builder.Services.Configure<StreamElementsOptions>(builder.Configuration.GetSection(StreamElementsOptions.Section));

InfrastructureDI.Configure(builder.Services, builder.Configuration);

var app = builder.Build();

app.UseCors(p => p.WithOrigins(builder.Configuration.GetValue<string>("Cors:WebUrl")!)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());

app.MapHub<DonationNotificationHub>("/donationHub");

app.Run();
