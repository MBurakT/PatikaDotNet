using AutoMapper;
using WebApi.Entities;
using static WebApi.Operations.UserOperations.CreateUsers.CreateUserCommand;

namespace WebApi.Utilities.Profiles;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<CreateUserModel, User>();
    }
}