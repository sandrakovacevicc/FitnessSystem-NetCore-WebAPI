using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitnessSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            
            modelBuilder.Entity<User>()
            .HasKey(u => u.JMBG);

            modelBuilder.Entity<Admin>().ToTable("Admins");
            modelBuilder.Entity<Client>().ToTable("Clients");
            modelBuilder.Entity<Trainer>().ToTable("Trainers");
            

            modelBuilder.Entity<Client>().HasMany(c => c.Sessions).WithMany(e => e.Clients)
                .UsingEntity<Reservation>();
        }

       
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<MembershipPackage> MembershipPackages { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<TrainingProgram> TrainingPrograms { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
