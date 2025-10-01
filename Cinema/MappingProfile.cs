using System.Text;
using AutoMapper;
using Cinema.Models.Dto;
using Cinema.Models.Request;
using Cinema.Models.Response;

namespace Cinema
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            #region Film mapping
            CreateMap<FilmUploadRequest, FilmDto>()
                .ForMember(dest => dest.Poster, opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(src.Poster)));
            CreateMap<FilmDto, FilmsGetResponse>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.GenreString));
            #endregion

            #region Hall mapping
            CreateMap<HallUploadRequest, HallDto>();
            CreateMap<HallDto, HallsGetResponce>();
            #endregion
        }
    }
}
