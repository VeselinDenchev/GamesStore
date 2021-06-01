namespace Data
{
    using System;

    using Data.Seed;

    using Model;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using System.Reflection;

    public class GamesStoreDbContext : IdentityDbContext<User>
    {
        public DbSet<Game> Games { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<DiscountCode> DiscountCodes { get; set; }

        public DbSet<Order> Orders { get; set; }

        private IConfigurationRoot configuration;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            configuration = new ConfigurationBuilder().AddUserSecrets(Assembly.GetExecutingAssembly()).Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //SeedData seed = new SeedData();

            //modelBuilder = seed.GenerateModels(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}
