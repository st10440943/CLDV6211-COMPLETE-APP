using CLDV6211_PART1_BOOKING_APP.Models;
using CLDV6211PART_1_App.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // DbSets for entities
    public DbSet<Venue> Venue { get; set; }
    public DbSet<Event> Event { get; set; }
    public DbSet<Booking> Booking { get; set; }
    public DbSet<EventType> EventType { get; set; }


    // Override OnModelCreating to call the Seed method
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Call the Seed method to populate the database with initial data
        ApplicationDbContext.Seed(modelBuilder);
    }

    // Static method to seed data
    public static void Seed(ModelBuilder modelBuilder)
    {
        // Seed data for Venue
        modelBuilder.Entity<Venue>().HasData(
            new Venue { VenueID = 1, VenueName = "Cascades Shopping Mall", Location = "Cascades Boulevard, Pietermaritzburg, 3201", Capacity = 5000, ImageUrl = "http://example.com/images/cascades_mall.jpg" },
            new Venue { VenueID = 2, VenueName = "Royal Showgrounds", Location = "Royal Agricultural Showgrounds, Pietermaritzburg", Capacity = 30000, ImageUrl = "http://example.com/images/royal_showgrounds.jpg" },
            new Venue { VenueID = 3, VenueName = "Pietermaritzburg City Hall", Location = "City Hall, Pietermaritzburg, 3201", Capacity = 1200, ImageUrl = "http://example.com/images/city_hall.jpg" },
            new Venue { VenueID = 4, VenueName = "KZN Museum", Location = "237 Jabu Ndlovu Street, Pietermaritzburg", Capacity = 200, ImageUrl = "http://example.com/images/kzn_museum.jpg" },
            new Venue { VenueID = 5, VenueName = "Pietermaritzburg Oval", Location = "Pietermaritzburg Oval, Pietermaritzburg", Capacity = 2500, ImageUrl = "http://example.com/images/pmb_oval.jpg" }
        );

        // Seed data for Event
        modelBuilder.Entity<Event>().HasData(
            new Event { EventID = 1, EventName = "PMB Jazz Festival", EventDate = new DateTime(2025, 5, 20, 18, 0, 0), Description = "A weekend of live jazz performances by local and international artists.", VenueID = 1 },
            new Event { EventID = 2, EventName = "Royal Show", EventDate = new DateTime(2025, 6, 5, 9, 0, 0), Description = "A major agricultural show with exhibitions, competitions, and entertainment.", VenueID = 2 },
            new Event { EventID = 3, EventName = "City Hall Concert", EventDate = new DateTime(2025, 7, 10, 19, 30, 0), Description = "A classical music concert featuring renowned orchestras.", VenueID = 3 },
            new Event { EventID = 4, EventName = "Science Exhibition", EventDate = new DateTime(2025, 8, 15, 10, 0, 0), Description = "An exhibition showcasing the latest in science and technology.", VenueID = 4 },
            new Event { EventID = 5, EventName = "Pietermaritzburg Rugby Final", EventDate = new DateTime(2025, 9, 5, 15, 0, 0), Description = "The highly anticipated rugby final at the Pietermaritzburg Oval.", VenueID = 5 }
        );

        // Seed data for Booking (include Status)
        modelBuilder.Entity<Booking>().HasData(
            new Booking { BookingID = 1, EventID = 1, VenueID = 1, BookingDate = new DateTime(2025, 4, 15, 11, 0, 0), Status = "Confirmed" },
            new Booking { BookingID = 2, EventID = 2, VenueID = 2, BookingDate = new DateTime(2025, 4, 18, 14, 0, 0), Status = "Pending" },
            new Booking { BookingID = 3, EventID = 3, VenueID = 3, BookingDate = new DateTime(2025, 4, 20, 16, 0, 0), Status = "Confirmed" },
            new Booking { BookingID = 4, EventID = 4, VenueID = 4, BookingDate = new DateTime(2025, 4, 25, 10, 0, 0), Status = "Pending" },
            new Booking { BookingID = 5, EventID = 5, VenueID = 5, BookingDate = new DateTime(2025, 5, 1, 9, 0, 0), Status = "Confirmed" }
        );
    }
}
