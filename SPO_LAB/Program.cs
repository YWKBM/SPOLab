using App.Metrics.Formatters.Prometheus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host
    .UseMetricsWebTracking(options =>
    {
        options.OAuth2TrackingEnabled = true;
    })
    .UseMetricsEndpoints(options =>
    {
        options.EnvironmentInfoEndpointEnabled = false;
        options.MetricsTextEndpointOutputFormatter = new MetricsPrometheusTextOutputFormatter();
        options.MetricsEndpointOutputFormatter = new MetricsPrometheusProtobufOutputFormatter();
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
