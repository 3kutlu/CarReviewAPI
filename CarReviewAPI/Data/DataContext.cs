using Microsoft.EntityFrameworkCore;
using MovieReviewAPI.Models;

namespace MovieReviewAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieCategory> MovieCategories { get; set; }
        public DbSet<MovieDirector> MovieDirectors { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieCategory>()
                .HasKey(mc => new { mc.MovieId, mc.CategoryId });
            modelBuilder.Entity<MovieCategory>()
                .HasOne(m => m.Movie)
                .WithMany(mc => mc.MovieCategories)
                .HasForeignKey(m => m.MovieId);
            modelBuilder.Entity<MovieCategory>()
                .HasOne(m => m.Category)
                .WithMany(mc => mc.MovieCategories)
                .HasForeignKey(c => c.CategoryId);


            modelBuilder.Entity<MovieDirector>()
                .HasKey(md => new { md.MovieId, md.DirectorId });
            modelBuilder.Entity<MovieDirector>()
                    .HasOne(m => m.Movie)
                    .WithMany(md => md.MovieDirectors)
                    .HasForeignKey(m => m.MovieId);
            modelBuilder.Entity<MovieDirector>()
                    .HasOne(m => m.Director)
                    .WithMany(md => md.MovieDirectors)
                    .HasForeignKey(d => d.DirectorId);
        }
    }
}
