using AutoMapper;
using SeminarTest.DTO;
using SeminarTest.Models;

namespace SeminarTest.MapProfile
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<ToDo, ToDoDTO>();
        }
    }
}
