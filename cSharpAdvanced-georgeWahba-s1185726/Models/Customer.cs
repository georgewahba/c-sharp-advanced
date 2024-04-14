namespace cSharpAdvanced_georgeWahba_s1185726.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
