using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mysqlx;
using SeminarTest.DTO;
using SeminarTest.Models;
using SeminarTest.Service;
using SeminarTest.Services;

namespace SeminarTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoesControllers : ControllerBase
    {
        private readonly IToDoService _service;
        public ToDoesControllers(IToDoService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<Response<List<ToDoDTO>>>> GetAll()
        {
            var listToDo = await _service.GetListToDo();
            if (listToDo == null)
            {
                return NotFound();
            }
            else
            {
                return Ok( new Response<List<ToDoDTO>>()
                {
                    Data = listToDo
                });
            }
        }
        [HttpPost]
        public async Task<ActionResult<Response<ToDoDTO>>> AddNewToDo(ToDoDTO toDoDTO)
        {
            var toDo = await _service.AddNewToDo(toDoDTO);
            if (toDo != null) 
            {
                return Ok(new Response<ToDoDTO>()
                {
                    Data = toDo
                });
            }
            else
            {
                return BadRequest();
            }    
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Response<ToDoDTO>>> EditToDo(ToDoDTO toDoDTO, Guid id)
        {
            if (EditToDo(toDoDTO,id) == null)
            {
                return NotFound();
            }
            else
            {
                await _service.EditExistToDo(toDoDTO, id);
                return Ok();
            }
            
        }
    }
}
