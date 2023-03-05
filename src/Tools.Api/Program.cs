
using IGeekFan.FreeKit.Extras.CaseQuery;
using IGeekFan.FreeKit.Extras.FreeSql;
using IGeekFan.FreeKit.Infrastructure.Filters;
using Microsoft.Extensions.Configuration;
using Tools.Api;

var builder = WebApplication.CreateBuilder(args);
var c = builder.Configuration;

// Add services to the container.
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services
    .AddSwagger(c)
    .AddCustomMVC(c);


var app = builder.Build();

string vPath = c.GetValue("VirtualPath", "");
app.UsePathBase(new PathString(vPath));
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint($"{vPath}/swagger/v1/swagger.json", "Tools API");
});

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
