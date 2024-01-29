using AutoMapper;
using WebApi.Entities;
using static WebApi.Utilities.Operations.GenreOperations.CreateGenres.CreateGenreCommand;
using static WebApi.Utilities.Operations.GenreOperations.GetGenres.GetGenreCommand;
using static WebApi.Utilities.Operations.GenreOperations.GetGenres.GetGenresQuery;
using static WebApi.Utilities.Operations.GenreOperations.UpdateGenres.UpdateGenreCommand;

namespace WebApi.Utilities.Profiles;

public class GenreMappingProfile : Profile
{
    public GenreMappingProfile()
    {
        CreateMap<Genre, GenreQueryViewModel>();
        CreateMap<Genre, GenreCommandViewModel>();
        CreateMap<CreateGenreViewModel, Genre>();
        CreateMap<UpdateGenreViewModel, Genre>();
    }
}