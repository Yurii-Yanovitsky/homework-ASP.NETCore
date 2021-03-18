using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebSurveyApp
{

    public class SurveyDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Response> Responses { get; set; }
        public DbSet<Report> Reports { get; set; }

        public SurveyDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Surveys)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade); ;

            modelBuilder.Entity<Survey>()
                .HasMany(s => s.Reports)
                .WithOne(q => q.Survey)
                .HasForeignKey(q => q.SurveyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Survey>()
                .HasMany(s => s.Questions)
                .WithOne(q => q.Survey)
                .HasForeignKey(q => q.SurveyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Survey>()
                .Property(s => s.Modified)
                .HasDefaultValueSql("GETDATE()");


            modelBuilder.Entity<Question>()
                .HasMany(qe => qe.Options)
                .WithOne(o => o.Question)
                .HasForeignKey(o => o.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Response>()
                .HasOne(r => r.Question)
                .WithMany(qe => qe.Responses)
                .HasForeignKey(r => r.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Response>()
                .HasOne(r => r.Option)
                .WithMany(qe => qe.Responses)
                .HasForeignKey(r => r.OptionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Response>()
                .HasAlternateKey(r => new { r.QuestionId, r.OptionId, r.ReportId });

            modelBuilder.Entity<Response>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Report>()
                .HasMany(ques => ques.Responses)
                .WithOne(r => r.Report)
                .HasForeignKey(r => r.ReportId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Option>()
                .Property(op => op.Text)
                .IsRequired(true);
        }
    }
}
