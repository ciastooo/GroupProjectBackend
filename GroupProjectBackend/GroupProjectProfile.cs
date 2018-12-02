using AutoMapper;
using GroupProjectBackend.Models.DB;
using GroupProjectBackend.Models.Dto;

public class GroupProjectProfile: Profile
{
    public GroupProjectProfile()
    {
        CreateMap<PlaceDto, Place>()
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Label))
            .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Position.Lat))
            .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Position.Lng))
            .ForMember(dest => dest.AverageRating, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.Ignore())
            .ForMember(dest => dest.FromRoutePlaces, opt => opt.Ignore())
            .ForMember(dest => dest.ToRoutePlaces, opt => opt.Ignore())
            .ForMember(dest => dest.UserRatings, opt => opt.Ignore());

    }
}