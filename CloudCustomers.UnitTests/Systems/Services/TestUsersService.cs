using CloudCustomers.API.Configs;
using CloudCustomers.API.Models;
using CloudCustomers.API.Services;
using CloudCustomers.UnitTests.Fixtures;
using CloudCustomers.UnitTests.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudCustomers.UnitTests.Systems.Services
{
    public class TestUsersService
    {
        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokeHttpGetRequest()
        {
            //Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);
            var endPoint = "https://example.com/users";
            var config = Options.Create(new UserApiOptions
            {
                EndPoint = endPoint
            });
            var sut = new UsersService(httpClient, config);

            //Act
            await sut.GetAllUsers();

            //Assert
            handlerMock.Protected().Verify(
                "SendAsync", 
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
                );

            //Verify Http request is made
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnsListOfUsers()
        {
            //Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);
            var endPoint = "https://example.com/users";
            var config = Options.Create(new UserApiOptions
            {
                EndPoint = endPoint
            });
            var sut = new UsersService(httpClient, config);

            //Act
            var result = await sut.GetAllUsers();

            //Assert
            result.Should().BeOfType<List<User>>();
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnsListOfUsersNotFound()
        {
            //Arrange
            var handlerMock = MockHttpMessageHandler<User>.SetupReturn404();
            var httpClient = new HttpClient(handlerMock.Object);
            var endPoint = "https://example.com/users";
            var config = Options.Create(new UserApiOptions
            {
                EndPoint = endPoint
            });
            var sut = new UsersService(httpClient, config);

            //Act
            var result = await sut.GetAllUsers();

            //Assert
            result.Count.Should().Be(0);
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnsListOfUsersOfExpectedSize()
        {
            //Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);
            var endPoint = "https://example.com/users";
            var config = Options.Create(new UserApiOptions
            {
                EndPoint = endPoint
            });
            var sut = new UsersService(httpClient, config);

            //Act
            var result = await sut.GetAllUsers();

            //Assert
            result.Count.Should().Be(expectedResponse.Count);
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokeConfiguredExternalUrl()
        {
            //Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var endPoint = "https://example.com/users";
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse, endPoint);
            var httpClient = new HttpClient(handlerMock.Object);

            var config = Options.Create(new UserApiOptions
            {
                EndPoint = endPoint
            });
            var sut = new UsersService(httpClient, config);

            //Act
            var result = await sut.GetAllUsers();
            var uri = new Uri(endPoint);

            //Assert
            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri == uri),
                ItExpr.IsAny<CancellationToken>()
                );
        }
    }
}
