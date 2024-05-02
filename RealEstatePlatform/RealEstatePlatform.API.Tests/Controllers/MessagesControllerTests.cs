using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RealEstatePlatform.API.Controllers;
using RealEstatePlatform.Application.Features.Messages.Commands.CreateMessage;
using RealEstatePlatform.Application.Features.Messages.Queries.GetAll;
using RealEstatePlatform.Application.Features.Messages.Queries.GetById;
using Xunit;

namespace RealEstatePlatform.API.Tests.Controllers
{
    public class MessagesControllerTests
    {
        [Fact]
        public async Task Create_ValidCommand_ReturnsOkResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var controller = new MessagesController(mediatorMock.Object);
            var createCommand = new CreateMessageCommand { MessageContent="string", ReceiverUsername="Test1", SenderUsername="Test2"};

            mediatorMock.Setup(m => m.Send(createCommand, CancellationToken.None))
                        .ReturnsAsync(new CreateMessageCommandResponse { Success = true });

            // Act
            var result = await controller.Create(createCommand);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<CreateMessageCommandResponse>(okResult.Value);
            Assert.True(response.Success);
            mediatorMock.Verify(m => m.Send(createCommand, CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task Create_InvalidCommand_ReturnsBadRequestResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var controller = new MessagesController(mediatorMock.Object);
            var createCommand = new CreateMessageCommand();

            mediatorMock.Setup(m => m.Send(createCommand, CancellationToken.None))
                        .ReturnsAsync(new CreateMessageCommandResponse { Success = false });

            // Act
            var result = await controller.Create(createCommand);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var response = Assert.IsType<CreateMessageCommandResponse>(badRequestResult.Value);
            Assert.False(response.Success);
            mediatorMock.Verify(m => m.Send(createCommand, CancellationToken.None), Times.Once);
        }


    }
}
