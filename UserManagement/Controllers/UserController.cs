using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserManagement.DTO;
using UserManagement.Facade.Interfaces.Services;
using UserManagement.Models;

namespace UserManagement.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IUserAccountService _userAccountService;
    private readonly IMapper _mapper;

    public UserController(IUserAccountService userAccountService, IUserService userService, IMapper mapper)
    {
        _userAccountService = userAccountService;
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult GetUser(int id)
    {
        var model = _mapper.Map<UserProfileModel>(_userService.GetById(id));

        if (model == null) return NotFound();

        return Ok(model);
    }

    [HttpPut]
    [Route("update")]
    public IActionResult Update(UserProfileModel model)
    {
        var userProfile = _mapper.Map<UserProfile>(model);
        _userService.Update(userProfile);

        return Ok($"User profile successfully updated");
    }

    [HttpPut]
    [Route("changePassword")]
    public IActionResult ChangePassword(UserModel model)
    {
        _userAccountService.UpdatePassword(model.UserId, model.Password);

        return Ok($"Password changed successfully on user with ID:{model.UserId}");
    }

    [HttpDelete]
    [Route("{id}")]
    public void Unregister(int id) => _userAccountService.Unregister(id);
}
