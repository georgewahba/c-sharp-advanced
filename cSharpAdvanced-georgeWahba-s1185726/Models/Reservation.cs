﻿namespace cSharpAdvanced_georgeWahba_s1185726.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int LocationId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Location Location { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public float Discount { get; set; }
    }
}
