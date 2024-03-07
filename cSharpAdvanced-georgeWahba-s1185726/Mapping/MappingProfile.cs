using AutoMapper;
using cSharpAdvanced_georgeWahba_s1185726.DTOs;
using cSharpAdvanced_georgeWahba_s1185726.Models;
using System.Linq;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Location, LocationDTO>()
            .ForMember(dest => dest.ImageURL, opt => opt.MapFrom(src => GetImageURL(src)))
            .ForMember(dest => dest.LandlordAvatarURL, opt => opt.MapFrom(src => GetLandlordAvatarUrl(src)));
    }

    private string GetImageURL(Location src)
    {
        if (src.Images != null)
        {
            var coverImage = src.Images.FirstOrDefault(img => img.IsCover);
            if (coverImage != null)
            {
                return coverImage.Url;
            }
        }
        return null;
    }

    private string GetLandlordAvatarUrl(Location src)
    {
        if (src.Landlord != null && src.Landlord.Avatar != null)
        {
            return src.Landlord.Avatar.Url;
        }
        return null;
    }
}
