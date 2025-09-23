namespace TodoListApp.Services.Mappings
{
    using AutoMapper;
    using TodoListApp.Entities.Entities;
    using TodoListApp.WebApi.Models.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Entity ↔ Model
            _ = this.CreateMap<Group, GroupModel>().ReverseMap();

            // Entity ↔ DTO
            //CreateMap<Group, GroupDto>().ReverseMap();

            // Можна робити окремі мапи для Create/Update DTO
            //CreateMap<GroupCreateDto, Group>();
            //CreateMap<GroupUpdateDto, Group>();
        }
    }
}
