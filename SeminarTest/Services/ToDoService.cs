using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SeminarTest.Data;
using SeminarTest.DTO;
using SeminarTest.Models;
using SeminarTest.Service;

namespace SeminarTest.Services
{
    public class ToDoService : IToDoService
    {
        private readonly ToDoContext _context;
        private readonly IMapper _mapper;
        public ToDoService(ToDoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ToDoDTO>?> GetListToDo()
        {
            try
            {
                var toDoList = await _context.ToDoList.Where(todo => !todo.IsDeleted).ToListAsync();

                //var toDoList = (from todo in _context.ToDoList where todo.UserId == userId where !todo.IsDeleted select todo).ToList();
                return _mapper.Map<List<ToDoDTO>>(toDoList);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<ToDoDTO> AddNewToDo(ToDoDTO toDoDTO)
        {
            try
            {
                var toDo = new ToDo()
                {
                    Title = toDoDTO.Title,
                    IsCompleted = toDoDTO.IsCompleted,
                    CreatedAt = DateTime.Now,
                    DeletedAt = null,
                    CompletedAt = null,
                    IsDeleted = false
                };
                if (toDoDTO.Title != null)
                {
                    _context.ToDoList.Add(toDo);
                    await _context.SaveChangesAsync();
                    return _mapper.Map<ToDoDTO>(toDo);
                }
                else
                    return null;   
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<ToDoDTO> EditExistToDo(ToDoDTO toDo, Guid toDoId)
        {
            try
            {
                var oldToDo = _context.ToDoList.Find(toDoId);
                if (oldToDo != null)
                {
                    oldToDo.Title = toDo.Title;
                    oldToDo.IsCompleted = toDo.IsCompleted;
                    if (oldToDo.IsCompleted)
                    {
                        oldToDo.CompletedAt = DateTime.Now;
                    }
                    _context.ToDoList.Update(oldToDo);
                }
                await _context.SaveChangesAsync();
                return _mapper.Map<ToDoDTO>(oldToDo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task DeleteToDo(Guid toDoId)
        {
            try
            {
                var toDoDelete = _context.ToDoList.Where(toDo => toDo.Id == toDoId).FirstOrDefault();
                if (toDoDelete != null)
                {
                    toDoDelete.IsDeleted = true;
                    toDoDelete.DeletedAt = DateTime.Now;
                    _context.ToDoList.Update(toDoDelete);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<ToDoDTO?> FindToDo(Guid toDoId)
        {
            try
            {
                var toDo = _mapper.Map<ToDoDTO>(await _context.ToDoList.FindAsync(toDoId));
                return toDo;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
