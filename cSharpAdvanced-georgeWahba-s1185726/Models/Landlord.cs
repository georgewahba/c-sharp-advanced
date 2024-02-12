namespace cSharpAdvanced_georgeWahba_s1185726.Models
{
    public class Landlord
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        // Foreign key for Avatar
        public int? AvatarId { get; set; } // Nullable if Avatar is optional

        // Navigation property for Avatar (Image)
        public Image Avatar { get; set; }

        // Navigation property for locations
        public ICollection<Location> Locations { get; set; }
    }
}
