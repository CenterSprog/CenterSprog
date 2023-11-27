using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly IAuthLogic _authLogic;
    
    public AuthController(IConfiguration config, IAuthLogic authLogic)
    {
        _config = config;
        _authLogic = authLogic;
    }
    
}