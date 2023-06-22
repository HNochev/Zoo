using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Zoo.Infrastructure.Data.Models;

namespace Zoo.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<WebsiteUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<News> News { get; set; }

        public DbSet<NewsComment> NewsComments { get; set; }

        public DbSet<Photo> Photos { get; set; }

        public DbSet<PhotoComment> PhotoComments { get; set; }

        public DbSet<Animal> Animals { get; set; }

        public DbSet<WebsiteUser> WebsiteUsers { get; set; }

        public DbSet<AnimalCondition> AnimalConditions { get; set; }

        public DbSet<PhotoStatus> PhotoStatuses { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Download> Downloads { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<BuyedTicket> BuyedTickets { get; set; }

        public DbSet<AnimalFeeding> AnimalFeedings { get; set; }

        public DbSet<AnimalEatingType> AnimalEatingTypes { get; set; }

        public DbSet<AnimalKind> AnimalKinds { get; set; }

        public DbSet<Transport> Transports { get; set; }

        public DbSet<TransportLineType> TransportLineTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<NewsComment>()
                .HasOne(x => x.News)
                .WithMany(x => x.NewsComments)
                .HasForeignKey(x => x.NewsId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<PhotoComment>()
                .HasOne(x => x.Photo)
                .WithMany(x => x.PhotoComments)
                .HasForeignKey(x => x.PhotoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
               .Entity<Animal>()
               .HasOne(x => x.AnimalCondition)
               .WithMany(x => x.Animals)
               .HasForeignKey(x => x.AnimalConditionId)
               .OnDelete(DeleteBehavior.Restrict);

            builder
              .Entity<Photo>()
              .HasOne(x => x.PhotoStatus)
              .WithMany(x => x.Photos)
              .HasForeignKey(x => x.PhotoStatusId)
              .OnDelete(DeleteBehavior.Restrict);

            builder
              .Entity<BuyedTicket>()
              .HasOne(x => x.Ticket)
              .WithMany(x => x.BuyedTickets)
              .HasForeignKey(x => x.TicketId)
              .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}