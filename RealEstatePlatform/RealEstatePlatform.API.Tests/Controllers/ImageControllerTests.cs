using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RealEstatePlatform.API.Controllers;
using RealEstatePlatform.Application.Features.Images.Commands.CreateImage;
using RealEstatePlatform.Application.Features.Images.Commands.DeleteImage;
using RealEstatePlatform.Application.Features.Images.Commands.UpdateImage;

namespace RealEstatePlatform.API.Tests.Controllers
{
    public class ImagesControllerTests
    {
        [Fact]
        public async Task Create_ValidCommand_ReturnsCreatedResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var controller = new ImagesController(mediatorMock.Object);
            var createCommand = new CreateImageCommand { ImageData="link", PropertyId= Guid.NewGuid() };

            mediatorMock.Setup(m => m.Send(createCommand, CancellationToken.None))
                        .ReturnsAsync(new CreateImageCommandResponse { Success = true });

            // Act
            var result = await controller.Create(createCommand);

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(result);
            var response = Assert.IsType<CreateImageCommandResponse>(createdResult.Value);
            Assert.True(response.Success);
            mediatorMock.Verify(m => m.Send(createCommand, CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task Create_InValidCommand_ReturnsBadRequestResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var controller = new ImagesController(mediatorMock.Object);
            var createCommand = new CreateImageCommand { /* Adjust properties as needed */ };

            mediatorMock.Setup(m => m.Send(createCommand, CancellationToken.None))
                        .ReturnsAsync(new CreateImageCommandResponse { Success = false });

            // Act
            var result = await controller.Create(createCommand);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var response = Assert.IsType<CreateImageCommandResponse>(badRequestResult.Value);
            Assert.False(response.Success);
            mediatorMock.Verify(m => m.Send(createCommand, CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task Delete_ExistingImage_ReturnsNoContentResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var controller = new ImagesController(mediatorMock.Object);
            var imageId = Guid.NewGuid();

            mediatorMock.Setup(m => m.Send(It.IsAny<DeleteImageCommand>(), CancellationToken.None))
                        .ReturnsAsync(new DeleteImageCommandResponse { Success = true });

            // Act
            var result = await controller.Delete(imageId);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            mediatorMock.Verify(m => m.Send(It.IsAny<DeleteImageCommand>(), CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task Update_InValidCommand_ReturnsBadRequestResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var controller = new ImagesController(mediatorMock.Object);
            var updateCommand = new UpdateImageCommand { /* Adjust properties as needed */ };

            mediatorMock.Setup(m => m.Send(updateCommand, CancellationToken.None))
                        .ReturnsAsync(new UpdateImageCommandResponse { Success = false });

            // Act
            var result = await controller.Update(updateCommand);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var response = Assert.IsType<UpdateImageCommandResponse>(badRequestResult.Value);
            Assert.False(response.Success);
            mediatorMock.Verify(m => m.Send(updateCommand, CancellationToken.None), Times.Once);
        }
    }

}
