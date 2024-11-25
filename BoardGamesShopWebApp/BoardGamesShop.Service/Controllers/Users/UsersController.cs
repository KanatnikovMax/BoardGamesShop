using System.Text;
using AutoMapper;
using BoardGamesShop.BusinessLogic.Users.Entities;
using BoardGamesShop.BusinessLogic.Users.Exceptions;
using BoardGamesShop.BusinessLogic.Users.Managers;
using BoardGamesShopWebApp.Controllers.Users.Entities;
using BoardGamesShopWebApp.Validator.Users;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace BoardGamesShopWebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersManager _usersManager;
    private readonly IUsersProvider _usersProvider;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    
    public UsersController(IUsersManager usersManager, IUsersProvider usersProvider, IMapper mapper, ILogger logger)
    {
        _usersManager = usersManager;
        _usersProvider = usersProvider;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> Register([FromQuery] RegisterUserRequest request)
    {
        var validationResult = await new RegisterUserRequestValidator().ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(x => x.ErrorMessage);
            var stringBuilder = new StringBuilder();
            foreach (var error in errors)
                stringBuilder.AppendLine(error);
            _logger.Error(stringBuilder.ToString());
            return BadRequest(errors);
        }
        
        var createUserModel = _mapper.Map<CreateUserModel>(request);
        try
        {
            var userModel = await _usersManager.CreateUserAsync(createUserModel);
            return Ok(new UsersListResponse
            {
                Users = [userModel]
            });
        }
        catch (UserAlreadyExistsException e)
        {
            _logger.Error(e.Message);
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            _logger.Error(e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _usersProvider.GetAllUsersAsync();
        return Ok(new UsersListResponse
        {
            Users = users.OrderBy(u => u.Id).ToList()
        });
    }

    [HttpGet]
    [Route("filtered")]
    public async Task<IActionResult> GetFilteredUsers([FromQuery] UserFilter userFilter)
    {
        var userModelFilter = _mapper.Map<UserModelFilter>(userFilter);
        var users = await _usersProvider.GetAllUsersAsync(userModelFilter);
        return Ok(new UsersListResponse
        {
            Users = users.ToList()
        });
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetUserInfo([FromRoute] int id)
    {
        try
        {
            var user = await _usersProvider.GetUserInfoAsync(id);
            return Ok(user);
        }
        catch (UserNotFoundException e)
        {
            _logger.Error(e.Message);
            return NotFound(e.Message);
        }
    }

    [HttpDelete]
    [Route("[action]/{id:int}")]
    public async Task<IActionResult> Unregister([FromRoute] int id)
    {
        try
        {
            await _usersManager.DeleteUserAsync(id);
            return Ok("User deleted successfully");
        }
        catch (UserNotFoundException e)
        {
            _logger.Error(e.Message);
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            _logger.Error(e.Message);
            return BadRequest(e.Message);
        }
    }
    
    [HttpPut]
    [Route("update/{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromQuery] UpdateUserRequest request)
    {
        var validationResult = await new UpdateUserRequestValidator().ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(x => x.ErrorMessage);
            var stringBuilder = new StringBuilder();
            foreach (var error in errors)
                stringBuilder.AppendLine(error);
            _logger.Error(stringBuilder.ToString());
            return BadRequest(errors);
        }
        
        var updateUserModel = _mapper.Map<UpdateUserModel>(request);
        try
        {
            var userModel = await _usersManager.UpdateUserAsync(updateUserModel, id);
            return Ok(new UsersListResponse
            {
                Users = [userModel]
            });
        }
        catch (UserNotFoundException e)
        {
            _logger.Error(e.Message);
            return NotFound(e.Message);
        }
        catch (UserAlreadyExistsException e)
        {
            _logger.Error(e.Message);
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            _logger.Error(e.Message);
            return BadRequest(e.Message);
        }
    }
}