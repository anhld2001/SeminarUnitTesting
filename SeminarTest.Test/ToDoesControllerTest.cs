using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Core;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Moq;
using SeminarTest.Controllers;
using SeminarTest.DTO;
using SeminarTest.Models;
using SeminarTest.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace SeminarTest.Test
{
    public class ToDoesControllerTest
    {
        private readonly Mock<IToDoService> _serviceMock;
        private readonly ToDoesControllers _sut;
        public ToDoesControllerTest()
        {
            _serviceMock = new Mock<IToDoService>();
            _sut = new ToDoesControllers(_serviceMock.Object);
        }
        [Fact]
        public async void GetAll_ShouldReturnOk_WhenFound()
        {
            var listToDoFake = new List<ToDoDTO>()
            {
                new ToDoDTO()
                {
                    Id = Guid.Parse("6a7e3bc2-f22e-4666-a363-ca8e26b56ad7"),
                    Title = "Test",
                    IsCompleted = true,
                }
            };
            _serviceMock.Setup(x => x.GetListToDo()).ReturnsAsync(listToDoFake) ;
            //action
            var result = await _sut.GetAll();
            var response = result.Result as OkObjectResult;
            Assert.NotNull(response?.StatusCode);
            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
        }

       [Fact]
        public async void GetAll_ShouldReturnNotFound_WhenCantFound()
        {
            
            _serviceMock.Setup(x => x.GetListToDo().Result).Returns(() => null);
            //action
            var result = await _sut.GetAll();
            var response = result.Result as NotFoundResult;
            Assert.NotNull(response?.StatusCode);
            Assert.Equal((int)HttpStatusCode.NotFound, response.StatusCode);
        }
        [Fact]
        public async void AddNew_ShouldReturnOk_IfValid()
        {
            var toDoFake = new ToDoDTO()
            {
                Id = Guid.Parse("6a7e3bc2-f22e-4666-a363-ca8e26b56ad7"),
                Title = "Test",
                IsCompleted = true
            };
            _serviceMock.Setup(x => x.AddNewToDo(toDoFake)).ReturnsAsync(toDoFake);
            //action
            var result = await _sut.AddNewToDo(toDoFake);
            var respone = result.Result as OkObjectResult;
            Assert.NotNull(respone?.StatusCode);
            Assert.Equal((int)HttpStatusCode.OK, respone.StatusCode);
        }
        [Fact]
        public async void AddNew_ShouldReturnBadRequest_IfNotValid()
        {
            var toDoFake = new ToDoDTO()
            {
                Id = Guid.Parse("6a7e3bc2-f22e-4666-a363-ca8e26b56ad7"),
                Title = null,
                IsCompleted = true
            };
            _serviceMock.Setup(x => x.AddNewToDo(toDoFake)).ReturnsAsync(() => null);
            //action
            var result = await _sut.AddNewToDo(toDoFake);
            var respone = result.Result as BadRequestResult;
            Assert.NotNull(respone?.StatusCode);
            Assert.Equal((int)HttpStatusCode.BadRequest, respone.StatusCode);
        }
        [Fact]
        public async void EditToDo_ShouldReturnOk_IfFound()
        {
            var id = Guid.Parse("6a7e3bc2-f22e-4666-a363-ca8e26b56ad7");
            var toDoFake = new ToDoDTO()
            {
                Id = Guid.Parse("6a7e3bc2-f22e-4666-a363-ca8e26b56ad7"),
                Title = "Abcd cai gi day cung duoc",
                IsCompleted = false
            };
            _serviceMock.Setup(x => x.EditExistToDo(toDoFake, id)).ReturnsAsync(toDoFake);
            //action
            var result = await _sut.EditToDo(toDoFake,id);
            var respone = result.Result as OkResult;
            Assert.NotNull(respone?.StatusCode);
            Assert.Equal((int)HttpStatusCode.OK, respone.StatusCode);
        }
        [Fact]
        public async void EditToDo_ShouldReturnNotFound_IfNotFound()
        {
            var id = Guid.Parse("6a7e3bc2-f22e-4666-a363-ca8e26b56ad7");
            var toDoFake = new ToDoDTO()
            {
                Id = Guid.Parse("6a7e3bc2-f22e-4666-a363-ca8e26b56ad7"),
                Title = "Abcd cai gi day cung duoc",
                IsCompleted = false
            };
            _serviceMock.Setup(x => x.EditExistToDo(toDoFake, id)).ReturnsAsync(() => null);
            //action
            var result = await _sut.EditToDo(toDoFake, id);
            var respone = result.Result as NotFoundResult;
            Assert.NotNull(respone?.StatusCode);
            Assert.Equal((int)HttpStatusCode.NotFound, respone.StatusCode);
        }
    }
}
