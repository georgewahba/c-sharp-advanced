namespace cSharpAdvanced_georgeWahba_s1185726.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public LocationType type { get; set; }
        public int Rooms { get; set; }
        public int NumberOfGuests { get; set; }
        public Features Features { get; set; }
        public Image Images { get; set; }
        public float PricePerDay { get; set; }
        public Reservation Reservations { get; set; }

        public Landlord landlord { get; set; }
    }
}