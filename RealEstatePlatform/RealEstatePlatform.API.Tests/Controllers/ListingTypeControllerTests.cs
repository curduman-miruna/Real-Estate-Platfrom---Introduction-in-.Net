using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RealEstatePlatform.API.Controllers;
using RealEstatePlatform.Application.Features.ListingTypes.Commands.CreateListingType;
using RealEstatePlatform.Application.Features.ListingTypes.Commands.DeleteListingType;
using RealEstatePlatform.Application.Features.ListingTypes.Commands.UpdateListingType;
using RealEstatePlatform.Application.Features.ListingTypes.Queries.GetAll;
using RealEstatePlatform.Domain.Common;
using RealEstatePlatform.Domain.Entities;

namespace RealEstatePlatform.API.Tests.Controllers
{
    public class ListingTypesControllerTests
    {
        [Fact]
        public async Task Create_ValidCommand_ReturnsOkResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var controller = new ListingTypesController(mediatorMock.Object);
            var createCommand = new CreateListingTypeCommand { ListingTypeName = "TestType" };

            mediatorMock.Setup(m => m.Send(createCommand, CancellationToken.None))
                        .ReturnsAsync(new CreateListingTypeCommandResponse { Success = true });

            // Act
            var result = await controller.Create(createCommand);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<CreateListingTypeCommandResponse>(okResult.Value);
            Assert.True(response.Success);
            mediatorMock.Verify(m => m.Send(createCommand, CancellationToken.None), Times.Once);
        }


        [Fact]
        public async Task Create_InValidCommand_ReturnsBadRequestResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var controller = new ListingTypesController(mediatorMock.Object);
            var createCommand = new CreateListingTypeCommand { ListingTypeName = "" }; // Invalid listing type name

            mediatorMock.Setup(m => m.Send(createCommand, CancellationToken.None))
                        .ReturnsAsync(new CreateListingTypeCommandResponse { Success = false });

            // Act
            var result = await controller.Create(createCommand);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var response = Assert.IsType<CreateListingTypeCommandResponse>(badRequestResult.Value);
            Assert.False(response.Success);
            mediatorMock.Verify(m => m.Send(createCommand, CancellationToken.None), Times.Once);
        }


        [Fact]
        public async Task Delete_ExistingListingType_ReturnsNoContentResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var controller = new ListingTypesController(mediatorMock.Object);
            var listingTypeId = Guid.NewGuid();

            mediatorMock.Setup(m => m.Send(It.IsAny<DeleteListingTypeCommand>(), CancellationToken.None))
                        .ReturnsAsync(new DeleteListingTypeCommandResponse { Success = true });

            // Act
            var result = await controller.Delete(listingTypeId);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            mediatorMock.Verify(m => m.Send(It.IsAny<DeleteListingTypeCommand>(), CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task Update_ValidCommand_ReturnsOkResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var controller = new ListingTypesController(mediatorMock.Object);
            var updateCommand = new UpdateListingTypeCommand { ListingTypeName = "TestType2" };

            mediatorMock.Setup(m => m.Send(updateCommand, CancellationToken.None))
                        .ReturnsAsync(new UpdateListingTypeCommandResponse { Success = true });

            // Act
            var result = await controller.Update(updateCommand);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<UpdateListingTypeCommandResponse>(okResult.Value);
            Assert.True(response.Success);
            mediatorMock.Verify(m => m.Send(updateCommand, CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task Update_InValidCommand_ReturnsBadRequestResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var controller = new ListingTypesController(mediatorMock.Object);
            var updateCommand = new UpdateListingTypeCommand(); // Invalid command without required data

            mediatorMock.Setup(m => m.Send(updateCommand, CancellationToken.None))
                        .ReturnsAsync(new UpdateListingTypeCommandResponse { Success = false });

            // Act
            var result = await controller.Update(updateCommand);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var response = Assert.IsType<UpdateListingTypeCommandResponse>(badRequestResult.Value);
            Assert.False(response.Success);
            mediatorMock.Verify(m => m.Send(updateCommand, CancellationToken.None), Times.Once);
        }



    }
}
