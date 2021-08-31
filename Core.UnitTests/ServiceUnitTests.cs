using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CodeChallenge.Controllers;
using Core.Library.Enums;
using Core.Library.Services;
using Core.Library.Models;
using Xunit;
using FluentAssertions;
using Moq;

namespace Core.UnitTests
{
	public class ServiceUnitTests
	{
		[Fact]
		public void Service_CreateNewService_ShouldSucceed()
		{
			// Arrange
			var mockService = new Mock<IService>();
			var serviceRequest = new ServiceRequestDto
			{
				BuildingCode = "SomeCode",
				Description = "SomeDesc"
			};
			var serviceResponse = new ServiceRequest
			{
				Id = Guid.NewGuid(),
				BuildingCode = serviceRequest.BuildingCode,
				Description = serviceRequest.Description,
				CurrentStatus = CurrentStatus.Created.ToString(),
				CreatedBy = "SomeUser",
				CreatedDate = DateTime.Now.Date.ToShortDateString(),
				LastModifiedBy = "SomeUser",
				LastModifiedDate = DateTime.Now.Date.ToShortDateString()
			};
			mockService.Setup(r => r.CreateNewService(serviceRequest)).Returns(serviceResponse);
			var serviceApi = new CodeChallengeController(mockService.Object);

			// Act
			var response = serviceApi.ServiceRequest(serviceRequest);

			// Assert
			response.Should().BeOfType<CreatedAtActionResult>();			
		}

		[Fact]
		public void Service_GetAllServices_ShouldBeInvokedOnce()
		{
			// Arrange
			var mockService = new Mock<IService>();
			var service = new ServiceRequest
			{
				Id = Guid.NewGuid(),
				BuildingCode = "SomeCode",
				Description = "SomeDesc",
				CurrentStatus = CurrentStatus.Created.ToString(),
				CreatedBy = "SomeUser",
				CreatedDate = DateTime.Now.Date.ToShortDateString(),
				LastModifiedBy = "SomeUser",
				LastModifiedDate = DateTime.Now.Date.ToShortDateString()
			};
			List<ServiceRequest> allServices = new List<ServiceRequest>();
			allServices.Add(service);
			mockService.Setup(r => r.GetAllServices()).Returns(allServices);
			var serviceApi = new CodeChallengeController(mockService.Object);

			// Act
			serviceApi.ServiceRequest();

			// Assert
			mockService.Verify(x => x.GetAllServices(), Times.Once);			
		}

		[Fact]
		public void Service_DeleteService_ShouldFail()
		{
			// Arrange
			var mockService = new Mock<IService>();
			Guid deleteGuid = Guid.Empty;
			mockService.Setup(r => r.DeleteService(deleteGuid)).Throws(new ArgumentException("Error parsing request"));
			var serviceApi = new CodeChallengeController(mockService.Object);

			// Act
			Action act = () => serviceApi.DeleteServiceRequest(deleteGuid);

			// Assert
			act.Should().Throw<ArgumentException>();
		}
	}
}
