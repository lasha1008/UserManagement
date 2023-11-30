using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserManagement.Api.JwtToken;
using UserManagement.DTO;
using UserManagement.Facade.Interfaces.Services;
using UserManagement.Models;

namespace UserManagement.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class AuthorizationController : ControllerBase
{
    private readonly IUserAccountService _userAccountService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public AuthorizationController(IUserAccountService userAccountService, IUserService userService, IMapper mapper, IConfiguration configuration)
    {
        _userAccountService = userAccountService;
        _userService = userService;
        _mapper = mapper;
        _configuration = configuration;
    }

    [HttpPost]
    [Route("register")]
    public IActionResult Register(UserRegisterModel model)
    {
        try
        {
            User user = _mapper.Map<User>(model);
            UserProfile userProfile = _mapper.Map<UserProfile>(model);

            _userAccountService.Register(user, userProfile);

            return Ok(GetToken(model.Username));
        }
        catch
        {
            return Unauthorized();
        }
    }

    [HttpPost]
    [Route("login")]
    public IActionResult Login(UserLoginModel model)
    {
        try
        {
            _userAccountService.Login(model.Username, model.Password);

            return Ok(GetToken(model.Username));
        }
        catch
        {
            return Unauthorized();
        }
    }

    [HttpGet]
    [Route("logout")]
    [CustomAuthorization]
    public IActionResult Logout()
    {
        var token = HttpContext.Request.Headers["Authorization"];
        TokenBlackList.RevokeToken(token!);

        return Ok("Logout successful");
    }


    private string GetToken(string username)
    {
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Email, username),
                new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
             }),
            Expires = DateTime.UtcNow.AddMinutes(30),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        tokenHandler.WriteToken(token);

        return tokenHandler.WriteToken(token);
    }
}
