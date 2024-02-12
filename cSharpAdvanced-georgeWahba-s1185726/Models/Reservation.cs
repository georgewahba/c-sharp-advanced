namespace cSharpAdvanced_georgeWahba_s1185726.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int LocationId { get; set; } // Foreign key

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Navigation property for location
        public Location Location { get; set; }

        // Other properties...
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public float Discount { get; set; }
    }
}
