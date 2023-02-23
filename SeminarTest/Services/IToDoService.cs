using SeminarTest.DTO;

namespace SeminarTest.Service
{
    public interface IToDoService
    {

        Task<List<ToDoDTO>?> GetListToDo();

        Task<ToDoDTO> AddNewToDo(ToDoDTO toDoDTO);

        Task<ToDoDTO> EditExistToDo(ToDoDTO toDoForm, Guid toDoId);

        Task DeleteToDo(Guid toDoId);

        Task<ToDoDTO>? FindToDo(Guid toDoId);
    }
}
