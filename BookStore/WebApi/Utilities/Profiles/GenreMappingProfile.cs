using AutoMapper;
using WebApi.Entities;
using static WebApi.Operations.GenreOperations.CreateGenres.CreateGenreCommand;
using static WebApi.Operations.GenreOperations.GetGenres.GetGenreCommand;
using static WebApi.Operations.GenreOperations.GetGenres.GetGenresQuery;
using static WebApi.Operations.GenreOperations.UpdateGenres.UpdateGenreCommand;

namespace WebApi.Utilities.Profiles;

class GenreMappingProfile : Profile
{
    public GenreMappingProfile()
    {
        CreateMap<Genre, GenreQueryViewModel>();
        CreateMap<Genre, GenreCommandViewModel>();
        CreateMap<CreateGenreViewModel, Genre>();
        CreateMap<UpdateGenreViewModel, Genre>();
    }
}