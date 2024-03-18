using cSharpAdvanced_georgeWahba_s1185726.Models;

namespace cSharpAdvanced_georgeWahba_s1185726.DTOs
{
    public class Location2DTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public string LandlordAvatarURL { get; set; }
        public float Price { get; set; }
        public Location.LocationType Type { get; set; }
    }
}
