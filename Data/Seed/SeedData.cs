namespace Data.Seed
{
    using Constants;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class SeedData
    {
        public SeedData(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public ModelBuilder modelBuilder { get; set; }

        public ModelBuilder GenerateModels() => this.modelBuilder;

        public void SeedRoles()
        {
            this.modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole 
                { 
                    Name = Role.ADMIN, 
                    NormalizedName = Role.ADMIN.ToUpper() 
                },
                new IdentityRole 
                {
                    Name = Role.USER,
                    NormalizedName = Role.USER.ToUpper()
                });
        }
    }
}
