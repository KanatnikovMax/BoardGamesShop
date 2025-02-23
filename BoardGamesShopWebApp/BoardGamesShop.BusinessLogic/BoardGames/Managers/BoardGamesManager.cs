using AutoMapper;
using BoardGamesShop.BusinessLogic.BoardGames.Entities;
using BoardGamesShop.BusinessLogic.BoardGames.Exceptions;
using BoardGamesShop.DataAccess.Entities;
using BoardGamesShop.DataAccess.Repository;

namespace BoardGamesShop.BusinessLogic.BoardGames.Managers;

public class BoardGamesManager : IBoardGamesManager
{
    private readonly IRepository<BoardGame> _boardGamesRepository;
    private readonly IMapper _mapper;

    public BoardGamesManager(IRepository<BoardGame> boardGamesRepository, IMapper mapper)
    {
        _boardGamesRepository = boardGamesRepository;
        _mapper = mapper;
    }


    public BoardGameModel CreateBoardGame(CreateBoardGameModel model)
    {
        var entity = _mapper.Map<BoardGame>(model);
        try
        {
            entity = _boardGamesRepository.Save(entity);
            return _mapper.Map<BoardGameModel>(entity);
        }
        catch (Exception e)
        {
            throw new BoardGameAlreadyExistsException("Board game exists");
        }
    }

    public async Task<BoardGameModel> CreateBoardGameAsync(CreateBoardGameModel model)
    {
        var entity = _mapper.Map<BoardGame>(model);
        try
        {
            entity = await _boardGamesRepository.SaveAsync(entity);
            return _mapper.Map<BoardGameModel>(entity);
        }
        catch (Exception e)
        {
            throw new BoardGameAlreadyExistsException("Board game exists");
        }
    }

    public void DeleteBoardGame(int id)
    {
        var entity = _boardGamesRepository.GetById(id);
        if (entity == null)
        {
            throw new BoardGameNotFoundException("Board game not found");
        }
        _boardGamesRepository.Delete(entity);
    }

    public async Task DeleteBoardGameAsync(int id)
    {
        var entity = await _boardGamesRepository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new BoardGameNotFoundException("Board game not found");
        }
        await _boardGamesRepository.DeleteAsync(entity);
    }

    public BoardGameModel UpdateBoardGame(UpdateBoardGameModel model, int id)
    {
        var entity = _boardGamesRepository.GetById(id);
        if (entity == null)
        {
            throw new BoardGameNotFoundException("Board game not found");
        }
        
        entity = _mapper.Map<UpdateBoardGameModel, BoardGame>(model, opts => opts.AfterMap(
            (src, dest) =>
            {
                dest.Id = id;
                dest.ExternalId = entity.ExternalId;
                dest.ModificationTime = entity.ModificationTime;
                dest.CreationTime = entity.CreationTime;
                dest.Name = entity.Name;
                dest.Genre = entity.Genre;
                dest.Price = src.Price is null ? entity.Price : (int)src.Price;
                dest.Publisher = entity.Publisher;
                dest.MinAge = entity.MinAge;
                dest.Description = src.Description is null ? entity.Description : src.Description;
            }));
        try
        {
            entity = _boardGamesRepository.Save(entity);
            return _mapper.Map<BoardGameModel>(entity);
        }
        catch (Exception e)
        {
            throw new BoardGameAlreadyExistsException("Board game exists");
        }
    }

    public async Task<BoardGameModel> UpdateBoardGameAsync(UpdateBoardGameModel model, int id)
    {
        var entity = await _boardGamesRepository.GetByIdAsync(id);
        if (entity == null)
        {
            throw new BoardGameNotFoundException("Board game not found");
        }
        
        entity = _mapper.Map<UpdateBoardGameModel, BoardGame>(model, opts => opts.AfterMap(
            (src, dest) =>
            {
                dest.Id = id;
                dest.ExternalId = entity.ExternalId;
                dest.ModificationTime = entity.ModificationTime;
                dest.CreationTime = entity.CreationTime;
                dest.Name = entity.Name;
                dest.Genre = entity.Genre;
                dest.Price = src.Price is null ? entity.Price : (int)src.Price;
                dest.Publisher = entity.Publisher;
                dest.MinAge = entity.MinAge;
                dest.Description = src.Description is null ? entity.Description : src.Description;
            }));
        try
        {
            entity = await _boardGamesRepository.SaveAsync(entity);
            return _mapper.Map<BoardGameModel>(entity);
        }
        catch (Exception e)
        {
            throw new BoardGameAlreadyExistsException("Board game exists");
        }
    }
}