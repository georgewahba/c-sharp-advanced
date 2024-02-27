using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace cSharpAdvanced_georgeWahba_s1185726.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public LocationType Type { get; set; }

        public int Rooms { get; set; }
        public int NumberOfGuests { get; set; }
        public Features Feature { get; set; }
        public float PricePerDay { get; set; }

        public int LandlordId { get; set; } // Foreign key

        // Navigation property for landlord
        public Landlord Landlord { get; set; }

        // Navigation property for reservations
        public ICollection<Reservation> Reservations { get; set; }

        // Navigation property for images
        public ICollection<Image> Images { get; set; }

        public enum LocationType
        {
            Apartment,
            Cottage,
            Chalet,
            Room,
            Hotel,
            House
        }

        [Flags]
        public enum Features
        {
            Smoking = 1,
            PetsAllowed = 2,
            Wifi = 4,
            TV = 8,
            Bath = 16,
            Breakfast = 32
        }
    }
}