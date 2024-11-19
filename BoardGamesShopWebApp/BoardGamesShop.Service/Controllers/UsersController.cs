using AutoMapper;
using BoardGamesShop.BusinessLogic.Users.Entities;
using BoardGamesShop.BusinessLogic.Users.Managers;
using BoardGamesShopWebApp.Controllers.Users.Entities;
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
        // TODO: тут должен вызываться валидатор
        var createUserModel = _mapper.Map<CreateUserModel>(request);
        try
        {
            var userModel = _usersManager.CreateUser(createUserModel);
            return Ok(new UsersListResponse
            {
                Users = [userModel]
            });
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
            Users = users.ToList()
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
}