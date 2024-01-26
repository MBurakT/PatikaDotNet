using AutoMapper;
using WebApi.Entities;
using static WebApi.Utilities.Operations.AuthorOperations.CreateAuthors.CreateAuthorCommand;
using static WebApi.Utilities.Operations.AuthorOperations.GetAuthors.GetAuthorCommand;
using static WebApi.Utilities.Operations.AuthorOperations.GetAuthors.GetAuthorsQuery;
using static WebApi.Utilities.Operations.AuthorOperations.UpdateAuthors.UpdateAuthorCommand;

namespace WebApi.Utilities.Profiles;

class AuthorMappingProfile : Profile
{
    public AuthorMappingProfile()
    {
        CreateMap<Author, GetAuthorsViewModel>();
        CreateMap<Author, GetAuthorViewModel>();
        CreateMap<CreateAuthorViewModel, Author>();
        CreateMap<UpdateAuthorViewModel, Author>();
    }
}