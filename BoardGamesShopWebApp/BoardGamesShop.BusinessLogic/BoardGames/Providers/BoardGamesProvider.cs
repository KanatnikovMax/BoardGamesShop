using AutoMapper;
using BoardGamesShop.BusinessLogic.BoardGames.Entities;
using BoardGamesShop.BusinessLogic.BoardGames.Exceptions;
using BoardGamesShop.DataAccess.Entities;
using BoardGamesShop.DataAccess.Repository;

namespace BoardGamesShop.BusinessLogic.BoardGames.Providers;

public class BoardGamesProvider : IBoardGamesProvider
{
    private readonly IRepository<BoardGame> _boardGamesRepository;
    private readonly IMapper _mapper;

    public BoardGamesProvider(IRepository<BoardGame> boardGamesRepository, IMapper mapper)
    {
        _boardGamesRepository = boardGamesRepository;
        _mapper = mapper;
    }

    public IEnumerable<BoardGameModel> GetAllBoardGames(BoardGameModelFilter filter = null)
    {
        var namePart = filter?.NamePart;
        var genrePart = filter?.GenrePart;
        var minPrice = filter?.MinPrice;
        var maxPrice = filter?.MaxPrice;
        var minAge = filter?.MinAge;
        var publisherPart = filter?.PublisherPart;
        
        var boardGames = _boardGamesRepository.GetAll(b =>
            (namePart == null || b.Name.Contains(namePart)) &&
            (genrePart == null || b.Genre.Contains(genrePart)) &&
            (minPrice == null || b.Price >= minPrice) &&
            (maxPrice == null || b.Price <= maxPrice) &&
            (minAge == null || b.MinAge == minAge) &&
            (publisherPart == null || b.Publisher.Contains(publisherPart)));
        
        return _mapper.Map<IEnumerable<BoardGameModel>>(boardGames);
    }

    public async Task<IEnumerable<BoardGameModel>> GetAllBoardGamesAsync(BoardGameModelFilter filter = null)
    {
        var namePart = filter?.NamePart;
        var genrePart = filter?.GenrePart;
        var minPrice = filter?.MinPrice;
        var maxPrice = filter?.MaxPrice;
        var minAge = filter?.MinAge;
        var publisherPart = filter?.PublisherPart;
        
        var boardGames = await _boardGamesRepository.GetAllAsync(b =>
            (namePart == null || b.Name.Contains(namePart)) &&
            (genrePart == null || b.Genre.Contains(genrePart)) &&
            (minPrice == null || b.Price >= minPrice) &&
            (maxPrice == null || b.Price <= maxPrice) &&
            (minAge == null || b.MinAge >= minAge) &&
            (publisherPart == null || b.Publisher.Contains(publisherPart)));
        
        return _mapper.Map<IEnumerable<BoardGameModel>>(boardGames);
    }

    public BoardGameModel GetBoardGameInfo(int userId)
    {
        var boardGame = _boardGamesRepository.GetById(userId)
                        ?? throw new BoardGameNotFoundException("Board game not fount");
        
        return _mapper.Map<BoardGameModel>(boardGame);
    }

    public async Task<BoardGameModel> GetBoardGameInfoAsync(int userId)
    {
        var boardGame = await _boardGamesRepository.GetByIdAsync(userId)
                        ?? throw new BoardGameNotFoundException("Board game not fount");
        
        return _mapper.Map<BoardGameModel>(boardGame);
    }
}