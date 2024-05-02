using AutoMapper;
using LibraryManager.Core.Dtos;
using LibraryManager.Core.Entities;
using LibraryManager.Core.ValueObjects;

namespace LibraryManager.Infrastructure.Services
{
    public class MappingService : Profile
    {
        public MappingService()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();

            CreateMap<Book, BookDTO>();
            CreateMap<BookDTO, Book>();

            CreateMap<LocationInfo, LocationInfoDTO>();
            CreateMap<LocationInfoDTO, LocationInfo>();

            CreateMap<Loan, LoanDTO>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.Book));
        }
    }
}
