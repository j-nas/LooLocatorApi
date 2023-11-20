using LooLocatorApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LooLocatorApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApiKeyController(IConfiguration configuration) : ControllerBase
{
    [HttpGet]
    public ActionResult<ApiKey> Get()
    {
        var apiKey = new ApiKey
        {
            Key = configuration["MapProviderApiKey:ApiKey"] ?? ""
        };
        return Ok(apiKey);
    }
}