using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TestApplicationDomain.Entities;

namespace TestApplicationDB
{
    public class TestApplicationContext : DbContext
    {
        public TestApplicationContext(DbContextOptions<TestApplicationContext> options) : base(options)
        {
        }

        public DbSet<Booking> Booking { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Facility> Facility { get; set; }
        public DbSet<Hotel> Hotel { get; set; }
        public DbSet<HotelContact> HotelContact { get; set; }
        public DbSet<Pricing> Pricing { get; set; }
        public DbSet<PricingDiscount> PricingDiscount { get; set; }
        public DbSet<PricingUserDiscount> PricingUserDiscount { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<RoomFacility> RoomFacility { get; set; }
        public DbSet<RoomFacilityType> RoomFacilityType { get; set; }
        public DbSet<RoomType> RoomType { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

    }
}
