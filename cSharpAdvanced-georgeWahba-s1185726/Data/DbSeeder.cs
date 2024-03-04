
using System;
using System.Linq;
using cSharpAdvanced_georgeWahba_s1185726.Models;

namespace cSharpAdvanced_georgeWahba_s1185726.Data
{
    public static class DbSeeder
    {
        public static void Seed(cSharpAdvanced_georgeWahba_s1185726Context context)
        {
            SeedCustomers(context);
            SeedLandlords(context);
            SeedLocations(context);
            SeedImages(context);
            SeedReservations(context);
        }

        private static void SeedCustomers(cSharpAdvanced_georgeWahba_s1185726Context context)
        {
            if (!context.Customer.Any())
            {
                var customers = new[]
                {
            new Customer { FirstName = "John", LastName = "Doe", Email = "john@example.com" },
            new Customer { FirstName = "Jane", LastName = "Smith", Email = "jane@example.com" },
            // Add more customers as needed
            new Customer { FirstName = "Alice", LastName = "Johnson", Email = "alice@example.com" },
            new Customer { FirstName = "Bob", LastName = "Williams", Email = "bob@example.com" },
            new Customer { FirstName = "Eva", LastName = "Brown", Email = "eva@example.com" },
            new Customer { FirstName = "Jack", LastName = "Jones", Email = "jack@example.com" },
            new Customer { FirstName = "Sophia", LastName = "Davis", Email = "sophia@example.com" },
            new Customer { FirstName = "Michael", LastName = "Miller", Email = "michael@example.com" },
            new Customer { FirstName = "Olivia", LastName = "Wilson", Email = "olivia@example.com" },
            new Customer { FirstName = "William", LastName = "Moore", Email = "william@example.com" }
        };
                context.Customer.AddRange(customers);
                context.SaveChanges();
            }
        }

        private static void SeedLandlords(cSharpAdvanced_georgeWahba_s1185726Context context)
        {
            if (!context.Landlord.Any())
            {
                var landlords = new[]
                {
            new Landlord { FirstName = "Michael", LastName = "Scott", Age = 45 },
            new Landlord { FirstName = "Dwight", LastName = "Schrute", Age = 40 },
            // Add more landlords as needed
            new Landlord { FirstName = "Pam", LastName = "Beesly", Age = 35 },
            new Landlord { FirstName = "Jim", LastName = "Halpert", Age = 36 },
            new Landlord { FirstName = "Stanley", LastName = "Hudson", Age = 60 },
            new Landlord { FirstName = "Angela", LastName = "Martin", Age = 38 },
            new Landlord { FirstName = "Kevin", LastName = "Malone", Age = 45 },
            new Landlord { FirstName = "Oscar", LastName = "Martinez", Age = 42 },
            new Landlord { FirstName = "Creed", LastName = "Bratton", Age = 60 },
            new Landlord { FirstName = "Ryan", LastName = "Howard", Age = 33 }
        };
                context.Landlord.AddRange(landlords);
                context.SaveChanges();
            }
        }

        private static void SeedLocations(cSharpAdvanced_georgeWahba_s1185726Context context)
        {
            if (!context.Location.Any())
            {
                var locations = new[]
                {
                    new Location { Title = "Cozy Apartment", SubTitle = "Downtown", Description = "A cozy apartment located in the heart of downtown.", Type = (Location.LocationType)(int)Location.LocationType.Apartment, Rooms = 2, NumberOfGuests = 4, Feature = (Location.Features)(int)(Location.Features.Wifi | Location.Features.Smoking), PricePerDay = 100, LandlordId = 1 },
                    new Location { Title = "Seaside Cottage", SubTitle = "Beachfront", Description = "A charming seaside cottage with direct access to the beach.", Type =(Location.LocationType)(int)Location.LocationType.Cottage, Rooms = 3, NumberOfGuests = 6, Feature = (Location.Features)(int)(Location.Features.PetsAllowed | Location.Features.Wifi), PricePerDay = 150, LandlordId = 2 },
                    new Location { Title = "Mountain Chalet", SubTitle = "Ski Resort", Description = "A cozy mountain chalet nestled in a ski resort area.", Type = (Location.LocationType)(int)Location.LocationType.Chalet, Rooms = 4, NumberOfGuests = 8, Feature = (Location.Features)(int)(Location.Features.TV | Location.Features.Bath), PricePerDay = 200, LandlordId = 3 },
                    new Location { Title = "Luxury Hotel Suite", SubTitle = "City Center", Description = "A luxurious hotel suite located in the bustling city center.", Type = (Location.LocationType)(int)Location.LocationType.Hotel, Rooms = 1, NumberOfGuests = 2, Feature =(Location.Features) (int)(Location.Features.Breakfast | Location.Features.Wifi), PricePerDay = 250, LandlordId = 4 },
                    new Location { Title = "Spacious House", SubTitle = "Suburban Area", Description = "A spacious house situated in a peaceful suburban area.", Type =(Location.LocationType)(int)Location.LocationType.House, Rooms = 5, NumberOfGuests = 10, Feature = (Location.Features)(int)(Location.Features.Wifi | Location.Features.PetsAllowed | Location.Features.TV), PricePerDay = 300, LandlordId = 5 }
                };
                context.Location.AddRange(locations);
                context.SaveChanges();
            }
        }


        private static void SeedImages(cSharpAdvanced_georgeWahba_s1185726Context context)
        {
            if (!context.Image.Any())
            {
                var images = new[]
                {
            new Image { Url = "https://example.com/image1.jpg", IsCover = true, LocationId = 1 },
            // Add more images as needed
            new Image { Url = "https://example.com/image2.jpg", IsCover = false, LocationId = 2 },
            new Image { Url = "https://example.com/image3.jpg", IsCover = false, LocationId = 3 },
            new Image { Url = "https://example.com/image4.jpg", IsCover = false, LocationId = 4 },
            new Image { Url = "https://example.com/image5.jpg", IsCover = false, LocationId = 5 }
        };
                context.Image.AddRange(images);
                context.SaveChanges();
            }
        }

        private static void SeedReservations(cSharpAdvanced_georgeWahba_s1185726Context context)
        {
            if (!context.Reservation.Any())
            {
                var reservations = new[]
                {
            new Reservation { StartDate = DateTime.Now.AddDays(1), EndDate = DateTime.Now.AddDays(7), LocationId = 1, CustomerId = 1, Discount = 0 },
            // Add more reservations as needed
            new Reservation { StartDate = DateTime.Now.AddDays(2), EndDate = DateTime.Now.AddDays(8), LocationId = 2, CustomerId = 2, Discount = 0 },
            new Reservation { StartDate = DateTime.Now.AddDays(3), EndDate = DateTime.Now.AddDays(9), LocationId = 3, CustomerId = 3, Discount = 0 },
            new Reservation { StartDate = DateTime.Now.AddDays(4), EndDate = DateTime.Now.AddDays(10), LocationId = 4, CustomerId = 4, Discount = 0 },
            new Reservation { StartDate = DateTime.Now.AddDays(5), EndDate = DateTime.Now.AddDays(11), LocationId = 5, CustomerId = 5, Discount = 0 }
        };
                context.Reservation.AddRange(reservations);
                context.SaveChanges();
            }
        }

    }
}