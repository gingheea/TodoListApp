namespace TodoListApp.Services.Mappings
{
    using AutoMapper;
    using TodoListApp.Contracts.DTO;
    using TodoListApp.Entities.Entities;
    using TodoListApp.WebApi.Models.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Entity ↔ Model
            _ = this.CreateMap<Group, GroupModel>().ReverseMap();
            _ = this.CreateMap<User, UserModel>().ReverseMap();
            _ = this.CreateMap<TodoItem, TodoItemModel>().ReverseMap();
            _ = this.CreateMap<TodoList, TodoListModel>().ReverseMap();

            // Entity ↔ DTO
            _ = this.CreateMap<Group, GroupDto>().ReverseMap();
            _ = this.CreateMap<User, UserDto>().ReverseMap();
            _ = this.CreateMap<TodoItem, TodoItemDto>().ReverseMap();
            _ = this.CreateMap<TodoList, TodoListDto>().ReverseMap();

            // Model ↔ DTO
            _ = this.CreateMap<GroupDto, GroupModel>().ReverseMap();
            _ = this.CreateMap<GroupDetailDto, GroupModel>().ReverseMap();
            _ = this.CreateMap<TodoListDto, TodoListModel>().ReverseMap();
            _ = this.CreateMap<TodoListDetailDto, TodoListModel>().ReverseMap();
            _ = this.CreateMap<UserDto, UserModel>().ReverseMap();
            _ = this.CreateMap<TodoItemDto, TodoItemModel>().ReverseMap();

            // Model ↔ Create/Update DTO
            _ = this.CreateMap<GroupCreateDto, GroupModel>();
            _ = this.CreateMap<GroupUpdateDto, GroupModel>();
            _ = this.CreateMap<UserUpdateDto, UserModel>();
            _ = this.CreateMap<TodoItemCreateDto, TodoItemModel>();
            _ = this.CreateMap<TodoItemUpdateDto, TodoItemModel>();
            _ = this.CreateMap<TodoListCreateDto, TodoListModel>();
            _ = this.CreateMap<TodoListUpdateDto, TodoListModel>();
        }
    }
}
