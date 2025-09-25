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

            // Model ↔ DTO
            _ = this.CreateMap<Group, GroupDto>().ReverseMap();

            // Model ↔ DTO
            _ = this.CreateMap<GroupDto, GroupModel>().ReverseMap();

            // Model ↔ Create/Update DTO
            _ = this.CreateMap<GroupCreateDto, GroupModel>();
            _ = this.CreateMap<GroupUpdateDto, GroupModel>();
        }
    }
}
