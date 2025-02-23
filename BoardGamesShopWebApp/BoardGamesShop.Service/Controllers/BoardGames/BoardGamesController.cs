using System.Text;
using AutoMapper;
using BoardGamesShop.BusinessLogic.BoardGames.Entities;
using BoardGamesShop.BusinessLogic.BoardGames.Exceptions;
using BoardGamesShop.BusinessLogic.BoardGames.Managers;
using BoardGamesShop.BusinessLogic.BoardGames.Providers;
using BoardGamesShop.DataAccess.Entities;
using BoardGamesShopWebApp.Controllers.BoardGames.Entities;
using BoardGamesShopWebApp.Validator.BoardGames;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace BoardGamesShopWebApp.Controllers.BoardGames;

[ApiController]
[Route("[controller]")]
public class BoardGamesController : ControllerBase
{
    private readonly IBoardGamesManager _boardGamesManager;
    private readonly IBoardGamesProvider _boardGamesProvider;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public BoardGamesController(IBoardGamesManager boardGamesManager, IBoardGamesProvider boardGamesProvider, 
        IMapper mapper, ILogger logger)
    {
        _boardGamesManager = boardGamesManager;
        _boardGamesProvider = boardGamesProvider;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost]
    [Route("create")]
    [Authorize]
    public async Task<ActionResult<BoardGame>> CreateBoardGame([FromQuery] CreateBoardGameRequest request)
    {
        var validationResult = await new CreateBoardGameRequestValidator().ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(error => error.ErrorMessage);
            var stringBuilder = new StringBuilder();
            foreach (var error in errors)
            {
                stringBuilder.AppendLine(error);
            }
            _logger.Error(stringBuilder.ToString());
            return BadRequest(errors);
        }
        
        var createBoardGameModel = _mapper.Map<CreateBoardGameModel>(request);
        try
        {
            var boardGameModel = await _boardGamesManager.CreateBoardGameAsync(createBoardGameModel);
            return Ok(new BoardGamesListResponse
            {
                BoardGames = [boardGameModel]
            });
        }
        catch (BoardGameAlreadyExistsException e)
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
    public async Task<IActionResult> GetAllBoardGames()
    {
        var boardGames = await _boardGamesProvider.GetAllBoardGamesAsync();
        return Ok(new BoardGamesListResponse
        {
            BoardGames = boardGames.ToList()
        });
    }
    
    [HttpGet]
    [Route("filtered")]
    public async Task<IActionResult> GetFilteredBoardGames([FromQuery] BoardGameFilter boardGameFilter)
    {
        var boardGameModelFilter = _mapper.Map<BoardGameModelFilter>(boardGameFilter);
        var boardGames = await _boardGamesProvider.GetAllBoardGamesAsync(boardGameModelFilter);
        return Ok(new BoardGamesListResponse
        {
            BoardGames = boardGames.ToList()
        });
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetBoardGameById([FromRoute] int id)
    {
        try
        {
            var boardGame = await _boardGamesProvider.GetBoardGameInfoAsync(id);
            return Ok(boardGame);
        }
        catch (BoardGameNotFoundException e)
        {
            _logger.Error(e.Message);
            return NotFound(e.Message);
        }
    }

    [HttpDelete]
    [Route("delete/{id:int}")]
    [Authorize]
    public async Task<IActionResult> DeleteBoardGame([FromRoute] int id)
    {
        try
        {
            await _boardGamesManager.DeleteBoardGameAsync(id);
            return Ok("Board game deleted successfully");
        }
        catch (BoardGameNotFoundException e)
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
    [Authorize]
    public async Task<IActionResult> UpdateBoardGame([FromRoute] int id, [FromQuery] UpdateBoardGameRequest request)
    {
        var validationResult = await new UpdateBoardGameRequestValidator().ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(error => error.ErrorMessage);
            var stringBuilder = new StringBuilder();
            foreach (var error in errors)
                stringBuilder.AppendLine(error);
            _logger.Error(stringBuilder.ToString());
            return BadRequest(errors);
        }
        
        var updateBoardGameModel = _mapper.Map<UpdateBoardGameModel>(request);
        try
        {
            var boardGameModel = await _boardGamesManager.UpdateBoardGameAsync(updateBoardGameModel, id);
            return Ok(new BoardGamesListResponse
            {
                BoardGames = [boardGameModel]
            });
        }
        catch (BoardGameNotFoundException e)
        {
            _logger.Error(e.Message);
            return NotFound(e.Message);
        }
        catch (BoardGameAlreadyExistsException e)
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