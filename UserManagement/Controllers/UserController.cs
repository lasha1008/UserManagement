using Microsoft.AspNetCore.Mvc;
using UserManagement.Facade.Interfaces.Repository;

namespace UserManagement.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    public UserController()
    {

    }
}
