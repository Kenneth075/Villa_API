//using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Serilog logger configuration.
//Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
//    .WriteTo.File("log/villalogs.txt", rollingInterval: RollingInterval.Day).CreateLogger();

//builder.Host.UseSerilog();  //This command tells the computer to use Serilog Logger instead of the build-in console log.


builder.Services.AddControllers(option => { 
    //option.ReturnHttpNotAcceptable = true; 
}).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();  //Adding xlm format to our api.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
