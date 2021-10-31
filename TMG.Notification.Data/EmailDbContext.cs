using Microsoft.EntityFrameworkCore;

using TMG.Notification.Data.Model;

namespace TMG.Notification.Data
{
  public partial class EmailDbContext : DbContext
  {
    public EmailDbContext(DbContextOptions<EmailDbContext> options)
            : base(options)
    {
    }

    public DbSet<EmailPurpose> EmailPurposes { get; set; }
    public DbSet<EmailTemplate> EmailTemplates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<EmailPurpose>(entity =>
      {
        entity.HasMany(x => x.EmailTemplates).WithOne(x => x.EmailPurpose).HasForeignKey(x => x.PurposeId);
      });
    }
  }
}
