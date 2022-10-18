using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Utilities.Encoders;
using System.Text;
using DSSM;

namespace Tools.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SM3Controller : ControllerBase
{
    private readonly ILogger<SM3Controller> _logger;

    public SM3Controller(ILogger<SM3Controller> logger)
    {
        _logger = logger;
    }



}