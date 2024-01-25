using AutoMapper;
using WebApi.Entities;
using WebApi.Operations.GenreOperations.GetGenres;
using WebApi.Operations.GenreOperations.CreateGenres;
using WebApi.Operations.GenreOperations.UpdateGenres;

namespace WebApi.Utilities.Profiles;

class GenreMappingProfile : Profile
{
    public GenreMappingProfile()
    {
        CreateMap<Genre, GetGenresQuery.GenreQueryViewModel>();
        CreateMap<Genre, GetGenreCommand.GenreCommandViewModel>();
        CreateMap<CreateGenreCommand.CreateGenreViewModel, Genre>();
        CreateMap<UpdateGenreCommand.UpdateGenreViewModel, Genre>();
    }
}