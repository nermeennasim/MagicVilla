

using MagicVilla_VillaAPI.Logging;

var builder = WebApplication.CreateBuilder(args);
/*//Add 3rd party Logger, with file name and logging interval
Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().
    WriteTo.File("log/villaLogs.txt",rollingInterval:RollingInterval.Day).CreateLogger();

builder.Host.UseSerilog();
*/

// Add services to the container.

builder.Services.AddControllers(
    options => { }
    //{options.ReturnHttpNotAcceptable = true; }
                )
    .AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddSingleton<ILogging, Logging>();

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
