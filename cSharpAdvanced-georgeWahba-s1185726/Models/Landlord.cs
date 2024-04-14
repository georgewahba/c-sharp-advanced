namespace cSharpAdvanced_georgeWahba_s1185726.Models
{
    public class Landlord
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int? AvatarId { get; set; }

        public Image Avatar { get; set; }

        public ICollection<Location> Locations { get; set; }
    }
}
