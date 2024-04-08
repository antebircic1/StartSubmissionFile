using Microsoft.EntityFrameworkCore;
using StartSubmissionFile.Models;

namespace StartSubmissionFile.Infrastructure.Persistance
{
	internal class SubmissionDbContext : DbContext
	{
		public DbSet<Submission> Submission { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Submission>(x =>
				x.HasKey(x => x.Id)
			);
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Server=.\;Database=Submission;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
		}
	}
}
