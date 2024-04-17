using DevFreela.Application.Queries.GetUser;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetUserQueryHandlerTests
    {
        [Fact]
        public async void DadoQueUsuarioExiste_Execute_RetorneUserViewModel()
        {
            //ARRANGE
            var user = new User("Nome Completo 1", "email@email.com", new DateTime(1989, 08,23), "123456", "cliente");
            var id = user.Id;

            var userRepositoryMock = new Mock<IUsersRepository>();
            userRepositoryMock.Setup(p => p.GetByIdAsync(id).Result).Returns(user);

            var getUserQuery = new GetUserQuery(id);
            var getUserQueryHandler = new GetUserQueryHandler(userRepositoryMock.Object);

            //ACT
            var userViewModel = await getUserQueryHandler.Handle(getUserQuery, new CancellationToken());

            //ASSERT
            Assert.NotNull(userViewModel);

            userRepositoryMock.Verify(us => us.GetByIdAsync(id).Result, Times.Once);

        }
    }
}
