using System.Linq.Expressions;
using BoardGamesShop.BusinessLogic.BoardGames.Providers;
using BoardGamesShop.BusinessLogic.UnitTests.Mapper;
using BoardGamesShop.DataAccess.Entities;
using BoardGamesShop.DataAccess.Repository;
using Moq;

namespace BoardGamesShop.BusinessLogic.UnitTests.BoardGames.Providers;

[TestFixture]
public class BoardGamesProviderTests
{
    [Test]
    public void TestGetAllBoardGames()
    {
        Expression expression = null;
        var repositoryMock = new Mock<IRepository<BoardGame>>();
        repositoryMock.Setup(repository => repository.GetAll(It.IsAny<Expression<Func<BoardGame, bool>>>()))
            .Callback((Expression<Func<BoardGame, bool>> x) => expression = x);
        var boardGamesProvider = new BoardGamesProvider(repositoryMock.Object, MapperHelper.Mapper);
        var result = boardGamesProvider.GetAllBoardGames();
        
        repositoryMock.Verify(repository => repository
            .GetAll(It.IsAny<Expression<Func<BoardGame, bool>>>()), Times.Once);
    }
}