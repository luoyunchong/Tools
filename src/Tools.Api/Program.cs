
using Microsoft.Extensions.Configuration;
using Tools.Api;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services
    .AddSwagger(configuration)
    .AddCustomMVC(configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors(policyName: "CorsPolicy");

app.UseRouting()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });


app.Run();
