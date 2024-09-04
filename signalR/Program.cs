using signalR.hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder.WithOrigins("http://localhost:3001")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    );
});
builder.Services.AddSignalR();
builder.Services.AddLogging();
builder.Services.AddHostedService<PeriodicMessageService>();
var app = builder.Build();


app.UseCors("CorsPolicy");
app.UseStaticFiles();
app.UseRouting();
app.MapHub<SignalHub>("/data");


app.MapFallbackToFile("index.html");

app.Run();
