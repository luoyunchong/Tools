using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Tools.Api.Application.Json;

namespace Tools.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JSONController : ControllerBase
{
    private readonly ILogger<JSONController> _logger;

    public JSONController(ILogger<JSONController> logger)
    {
        _logger = logger;
    }

    [HttpPost("GenerateClassFromJsonByNewtonsoft")]
    public string GenerateClassFromJsonByNewtonsoft([FromBody] GenerateCSharpClassInput input)
    {
        var json = JsonConvert.DeserializeObject<string>(input.Json);
        var res = JsonToCsharpByNewtonsoft.GenerateClassFromJson(json, input.ClassName);
        return res;
    }

    [HttpPost("GenerateCSharpClass")]
    public string GenerateCSharpClass([FromBody] GenerateCSharpClassInput input)
    {
        var json = System.Text.Json.JsonSerializer.Deserialize<string>(input.Json);
        var res = JsonToCSharpClassGenerator.GenerateClassFromJson(json, input.ClassName);
        return res;
    }
}

public record GenerateCSharpClassInput(string Json, string ClassName);