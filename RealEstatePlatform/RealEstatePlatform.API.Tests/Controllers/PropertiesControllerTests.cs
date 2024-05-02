using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RealEstatePlatform.API.Controllers;
using RealEstatePlatform.Application.Features.Properties.Commands.CreateProperty;
using RealEstatePlatform.Application.Features.Properties.Commands.UpdateProperty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePlatform.API.Tests.Controllers
{
    public class PropertiesControllerTests
    {
        [Fact]
        public async Task Create_ValidCommand_ReturnsCreatedResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var controller = new PropertiesController(mediatorMock.Object);
            var createCommand = new CreatePropertyCommand { /* Adjust properties as needed */ };

            mediatorMock.Setup(m => m.Send(createCommand, CancellationToken.None))
                        .ReturnsAsync(new CreatePropertyCommandResponse { Success = true });

            // Act
            var result = await controller.Create(createCommand);

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(result);
            var response = Assert.IsType<CreatePropertyCommandResponse>(createdResult.Value);
            Assert.True(response.Success);
            mediatorMock.Verify(m => m.Send(createCommand, CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task Create_InValidCommand_ReturnsBadRequestResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var controller = new PropertiesController(mediatorMock.Object);
            var createCommand = new CreatePropertyCommand { /* Adjust properties as needed */ };

            mediatorMock.Setup(m => m.Send(createCommand, CancellationToken.None))
                        .ReturnsAsync(new CreatePropertyCommandResponse { Success = false });

            // Act
            var result = await controller.Create(createCommand);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var response = Assert.IsType<CreatePropertyCommandResponse>(badRequestResult.Value);
            Assert.False(response.Success);
            mediatorMock.Verify(m => m.Send(createCommand, CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task Update_ValidCommand_ReturnsOkResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var controller = new PropertiesController(mediatorMock.Object);
            var propertyId = Guid.NewGuid();
            var updateCommand = new UpdatePropertyCommand { PropertyId = propertyId, /* Adjust properties as needed */ };

            mediatorMock.Setup(m => m.Send(updateCommand, CancellationToken.None))
                        .ReturnsAsync(new UpdatePropertyCommandResponse { Success = true });

            // Act
            var result = await controller.Update(propertyId, updateCommand);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<UpdatePropertyCommandResponse>(okResult.Value);
            Assert.True(response.Success);
            mediatorMock.Verify(m => m.Send(updateCommand, CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task Update_InValidCommand_ReturnsBadRequestResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var controller = new PropertiesController(mediatorMock.Object);
            var propertyId = Guid.NewGuid();
            var updateCommand = new UpdatePropertyCommand { PropertyId = propertyId, /* Adjust properties as needed */ };

            mediatorMock.Setup(m => m.Send(updateCommand, CancellationToken.None))
                        .ReturnsAsync(new UpdatePropertyCommandResponse { Success = false });

            // Act
            var result = await controller.Update(propertyId, updateCommand);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var response = Assert.IsType<UpdatePropertyCommandResponse>(badRequestResult.Value);
            Assert.False(response.Success);
            mediatorMock.Verify(m => m.Send(updateCommand, CancellationToken.None), Times.Once);
        }
    }

}
