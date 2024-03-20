using AutoMapper;
using cSharpAdvanced_georgeWahba_s1185726.DTOs;
using cSharpAdvanced_georgeWahba_s1185726.Models;
using System.Linq;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Location, LocationDTO>()
             .ForMember(dest => dest.ImageURL, opt => opt.MapFrom(src => GetImageURL(src.Images)))
             .ForMember(dest => dest.LandlordAvatarURL, opt => opt.MapFrom(src => GetLandlordAvatarUrl(src.Images)));

        CreateMap<Location, Location2DTO>()
            .ForMember(dest => dest.ImageURL, opt => opt.MapFrom(src => GetImageURL(src.Images)))
            .ForMember(dest => dest.LandlordAvatarURL, opt => opt.MapFrom(src => GetLandlordAvatarUrl(src.Images)))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.PricePerDay))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type));

        CreateMap<Location, LocationDetailsDTO>()
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
            .ForMember(dest => dest.Landlord, opt => opt.MapFrom(src => src.Landlord));
    }

    public static string GetImageURL(IEnumerable<Image> images)
    {
        if (images != null)
        {
            var coverImage = images.FirstOrDefault(img => img.IsCover);
            if (coverImage != null)
            {
                return coverImage.Url;
            }
        }
        return null;
    }

    public static string GetLandlordAvatarUrl(IEnumerable<Image> images)
    {
        if (images != null)
        {
            var avatarImage = images.FirstOrDefault(img => !img.IsCover);
            if (avatarImage != null)
            {
                return avatarImage.Url;
            }
        }
        return null;
    }
}
