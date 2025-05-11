using AutoMapper;
using TaskManager.API.Models;
using TaskManager.API.DTOs;

namespace TaskManager.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<RegisterUserDto, User>();

            CreateMap<TaskItem, TaskDto>();
            CreateMap<CreateTaskDto, TaskItem>();
            CreateMap<UpdateTaskDto, TaskItem>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
} 