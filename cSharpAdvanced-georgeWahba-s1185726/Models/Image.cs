namespace cSharpAdvanced_georgeWahba_s1185726.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsCover { get; set; }

        public int LocationId { get; set; } // Foreign key

        // Navigation property for location
        public Location Location { get; set; }
    }
}
