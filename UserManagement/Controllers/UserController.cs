using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Api.JwtToken;
using UserManagement.DTO;
using UserManagement.Facade.Interfaces.Services;
using UserManagement.Models;

namespace UserManagement.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[CustomAuthorization]
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

    [HttpGet]
    [Route("search/{text}")]
    public IActionResult search(string text)
    {
        var models = _userService.Search(text);

        if (models == null) return NotFound();

        return Ok(models);
    }

    [HttpPut]
    [Route("update/{id}")]
    public IActionResult Update(int id, UserProfileModel model)
    {
        UserProfile userProfile = _userService.Set(x => x.UserId == id).Single();

        userProfile.FirstName = model.FirstName;
        userProfile.LastName = model.LastName;
        userProfile.PersonalNumber = model.PersonalNumber;

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
    [Route("unregister/{id}")]
    public void Unregister(int id) => _userAccountService.Unregister(id);
}
