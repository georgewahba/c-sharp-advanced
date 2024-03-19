namespace cSharpAdvanced_georgeWahba_s1185726.DTOs
{
    public class LocationDetailsDTO
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public int Rooms { get; set; }
        public int NumberOfGuests { get; set; }
        public float PricePerDay { get; set; }
        public int Type { get; set; }
        public int Features { get; set; }
        public ICollection<ImageDTO> Images { get; set; }
        public LandlordDTO Landlord { get; set; }
    }

    public class ImageDTO
    {
        public string URL { get; set; }
        public bool IsCover { get; set; }
    }

    public class LandlordDTO
    {
        public string Name { get; set; }
        public string Avatar { get; set; }
    }
}
