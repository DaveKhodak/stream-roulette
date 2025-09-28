using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.AzureAppServices;
using stream_roulette.api.Services.Donations;
using stream_roulette.core;
using stream_roulette.core.Hubs;
using stream_roulette.infra;
using stream_roulette.infra.Persistence.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddAzureWebAppDiagnostics();
builder.Services.Configure<AzureFileLoggerOptions>(options =>
{
    options.FileName = "logs-";
    options.FileSizeLimit = 50 * 1024;
    options.RetainedFileCountLimit = 5;
});

builder.Services.AddHostedService<DonationsBackgroundService>();
builder.Services.AddControllers();
builder.Services.AddCors();

CoreDI.Configure(builder.Services, builder.Configuration);
InfraDI.Configure(builder.Services, builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    db.Database.Migrate();
}

app.UseCors(p => p.WithOrigins(builder.Configuration.GetValue<string>("Cors:WebUrl")!)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());

app.UseAuthentication();

app.MapHub<DonationNotificationHub>("/donationHub");

app.MapControllers();

app.Run();
