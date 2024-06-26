﻿using DevFreela.Application.Command.CreateProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DevFreela.UnitTests.Application.Commands
{
    public class CreateProjectCommandHandlerTests
    {
        [Fact]
        public async Task InputDataIsOk_Executed_ReturnProjectId()
        {
            // ARRANGE
            var projectRepository = new Mock<IProjectRepositoriy>();

            var createProjectCommand = new CreateProjectCommand
            {
                Title = "Titulo de Teste",
                Description = "Uma descrição Daora",
                TotalCost = 50000,
                IdClient = 1,
                IdFreelancer = 2
            };

            var createProjectCommandHandler = new CreateProjectCommandHandler(projectRepository.Object);

            // ACT
            var id = await createProjectCommandHandler.Handle(createProjectCommand, new CancellationToken());

            // ASSERT
            Assert.True(id >= 0);

            projectRepository.Verify(pr => pr.AddProject(It.IsAny<Project>()), Times.Once);
        }
    }
}
