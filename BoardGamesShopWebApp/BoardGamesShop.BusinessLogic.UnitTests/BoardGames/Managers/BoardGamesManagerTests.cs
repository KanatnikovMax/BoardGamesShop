using System.Linq.Expressions;
using BoardGamesShop.BusinessLogic.BoardGames.Entities;
using BoardGamesShop.BusinessLogic.BoardGames.Exceptions;
using BoardGamesShop.BusinessLogic.BoardGames.Managers;
using BoardGamesShop.BusinessLogic.UnitTests.Mapper;
using BoardGamesShop.DataAccess.Entities;
using BoardGamesShop.DataAccess.Repository;
using FluentAssertions;
using Moq;

namespace BoardGamesShop.BusinessLogic.UnitTests.BoardGames.Managers;

[TestFixture]
public class BoardGamesManagerTests
{
    [Test]
    public void BoardGamesManager_CreateBoardGame_Success()
    {
        var repositoryMock = new Mock<IRepository<BoardGame>>();
        repositoryMock.Setup(repository => repository.GetAll(It.IsAny<Expression<Func<BoardGame, bool>>>()))
            .Returns(new List<BoardGame>().AsQueryable());
        var externalId = Guid.NewGuid();
        repositoryMock.Setup(repository => repository.Save(It.IsAny<BoardGame>()))
            .Returns((BoardGame x) =>
            {
                x.Id = 1;
                x.CreationTime = DateTime.Now;
                x.ModificationTime = DateTime.Now;
                x.ExternalId = externalId;
                return x;
            });
        
        var mapper = MapperHelper.Mapper;
        
        var boardGamesManager = new BoardGamesManager(repositoryMock.Object, mapper);

        var createBoardGameModel = new CreateBoardGameModel
        {
            Name = "DnD",
            Description = "Самая популярная НРИ",
            Genre = "НРИ",
            Publisher = "Hobby World",
            Price = 3999,
            MinAge = 6
        };
        
        var boardGameModel = boardGamesManager.CreateBoardGame(createBoardGameModel);
        
        boardGameModel.Should().NotBeNull();
        boardGameModel.Id.Should().Be(1);
        boardGameModel.ExternalId.Should().Be(externalId);
        boardGameModel.Name.Should().Be(createBoardGameModel.Name);
        boardGameModel.Description.Should().Be(createBoardGameModel.Description);
        boardGameModel.Genre.Should().Be(createBoardGameModel.Genre);
        boardGameModel.Publisher.Should().Be(createBoardGameModel.Publisher);
        boardGameModel.Price.Should().Be(createBoardGameModel.Price);
        boardGameModel.MinAge.Should().Be(createBoardGameModel.MinAge);
    }
}