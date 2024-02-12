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
                    new Location { Title = "Cozy Apartment", SubTitle = "Downtown", Type = Location.LocationType.Apartment, Rooms = 2, NumberOfGuests = 4, Feature = Location.Features.Wifi, PricePerDay = 100, LandlordId = 1 },
                    // Add more locations as needed
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
                };
                context.Reservation.AddRange(reservations);
                context.SaveChanges();
            }
        }
    }
}
