using stream_roulette.api.Services.Donations;
using stream_roulette.core;
using stream_roulette.core.Hubs;
using stream_roulette.infra;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<DonationsBackgroundService>();
builder.Services.AddControllers();
builder.Services.AddCors();

CoreDI.Configure(builder.Services, builder.Configuration);
InfraDI.Configure(builder.Services, builder.Configuration);

var app = builder.Build();

app.UseCors(p => p.WithOrigins(builder.Configuration.GetValue<string>("Cors:WebUrl")!)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());

app.MapHub<DonationNotificationHub>("/donationHub");

app.MapControllers();

app.Run();
