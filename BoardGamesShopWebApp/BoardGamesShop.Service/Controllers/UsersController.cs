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
    public IActionResult Register([FromQuery] RegisterUserRequest request)
    {
        var validationResult = new RegisterUserRequestValidator().Validate(request);
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
            var userModel = _usersManager.CreateUser(createUserModel);
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
    public IActionResult GetAllUsers()
    {
        var users = _usersProvider.GetAllUsers();
        return Ok(new UsersListResponse
        {
            Users = users.OrderBy(u => u.Id).ToList()
        });
    }

    [HttpGet]
    [Route("filtered")]
    public IActionResult GetFilteredUsers([FromQuery] UserFilter userFilter)
    {
        var userFilterModel = _mapper.Map<UserModelFilter>(userFilter);
        var users = _usersProvider.GetAllUsers(userFilterModel);
        return Ok(new UsersListResponse
        {
            Users = users.ToList()
        });
    }

    [HttpGet]
    [Route("{id:int}")]
    public IActionResult GetUserInfo([FromRoute] int id)
    {
        try
        {
            var user = _usersProvider.GetUserInfo(id);
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
    public IActionResult Unregister([FromRoute] int id)
    {
        try
        {
            _usersManager.DeleteUser(id);
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
    public IActionResult Update([FromRoute] int id, [FromQuery] UpdateUserRequest request)
    {
        var validationResult = new UpdateUserRequestValidator().Validate(request);
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
            var userModel = _usersManager.UpdateUser(updateUserModel, id);
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