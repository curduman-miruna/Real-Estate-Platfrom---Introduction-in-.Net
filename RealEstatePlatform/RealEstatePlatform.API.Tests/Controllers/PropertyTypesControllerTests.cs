using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RealEstatePlatform.API.Controllers;
using RealEstatePlatform.Application.Features.PropertyTypes.Commands.CreatePropertyType;
using RealEstatePlatform.Application.Features.PropertyTypes.Commands.DeletePropertyType;
using RealEstatePlatform.Application.Features.PropertyTypes.Commands.UpdatePropertyType;
using RealEstatePlatform.Application.Features.PropertyTypes.Queries.GetAll;
using RealEstatePlatform.Application.Features.PropertyTypes.Queries.GetById;
using Xunit;

namespace RealEstatePlatform.API.Tests.Controllers
{
    public class PropertyTypesControllerTests
    {
        [Fact]
        public async Task Create_ValidCommand_ReturnsOkResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var controller = new PropertyTypesController(mediatorMock.Object);
            var createCommand = new CreatePropertyTypeCommand { PropertyTypeName = "TestType" };

            mediatorMock.Setup(m => m.Send(createCommand, CancellationToken.None))
                        .ReturnsAsync(new CreatePropertyTypeCommandResponse { Success = true });

            // Act
            var result = await controller.Create(createCommand);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<CreatePropertyTypeCommandResponse>(okResult.Value);
            Assert.True(response.Success);
            mediatorMock.Verify(m => m.Send(createCommand, CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task Create_InValidCommand_ReturnsBadRequestResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var controller = new PropertyTypesController(mediatorMock.Object);
            var createCommand = new CreatePropertyTypeCommand { PropertyTypeName = "" }; // Invalid property type name

            mediatorMock.Setup(m => m.Send(createCommand, CancellationToken.None))
                        .ReturnsAsync(new CreatePropertyTypeCommandResponse { Success = false });

            // Act
            var result = await controller.Create(createCommand);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var response = Assert.IsType<CreatePropertyTypeCommandResponse>(badRequestResult.Value);
            Assert.False(response.Success);
            mediatorMock.Verify(m => m.Send(createCommand, CancellationToken.None), Times.Once);
        }



        [Fact]
        public async Task Update_ValidCommand_ReturnsOkResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var controller = new PropertyTypesController(mediatorMock.Object);
            var updateCommand = new UpdatePropertyTypeCommand { PropertyTypeName = "TestType2" };

            mediatorMock.Setup(m => m.Send(updateCommand, CancellationToken.None))
                        .ReturnsAsync(new UpdatePropertyTypeCommandResponse { Success = true });

            // Act
            var result = await controller.Update(updateCommand);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<UpdatePropertyTypeCommandResponse>(okResult.Value);
            Assert.True(response.Success);
            mediatorMock.Verify(m => m.Send(updateCommand, CancellationToken.None), Times.Once);
        }


        [Fact]
        public async Task Update_InValidCommand_ReturnsBadRequestResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var controller = new PropertyTypesController(mediatorMock.Object);
            var updateCommand = new UpdatePropertyTypeCommand(); // Invalid command without required data

            mediatorMock.Setup(m => m.Send(updateCommand, CancellationToken.None))
                        .ReturnsAsync(new UpdatePropertyTypeCommandResponse { Success = false });

            // Act
            var result = await controller.Update(updateCommand);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var response = Assert.IsType<UpdatePropertyTypeCommandResponse>(badRequestResult.Value);
            Assert.False(response.Success);
            mediatorMock.Verify(m => m.Send(updateCommand, CancellationToken.None), Times.Once);
        }


        [Fact]
        public async Task Delete_ExistingPropertyType_ReturnsNoContentResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var controller = new PropertyTypesController(mediatorMock.Object);
            var propertyTypeId = Guid.NewGuid();

            mediatorMock.Setup(m => m.Send(It.IsAny<DeletePropertyTypeCommand>(), CancellationToken.None))
                        .ReturnsAsync(new DeletePropertyTypeCommandResponse { Success = true });

            // Act
            var result = await controller.Delete(propertyTypeId);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            mediatorMock.Verify(m => m.Send(It.IsAny<DeletePropertyTypeCommand>(), CancellationToken.None), Times.Once);
        }


    }
}



