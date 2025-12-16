using Microsoft.EntityFrameworkCore;

namespace MinorFeature.Model
{
    /// <summary>
    /// 管理平台DB
    /// </summary>
    public partial class MFAdminDbContext:DbContext
    {
        public MFAdminDbContext(DbContextOptions<MFAdminDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// 访问用户Db集合
        /// </summary>
        public virtual DbSet<AdminUser> AdminUsers { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminUser>(entity =>
            {
                modelBuilder.Entity<AdminUser>()
                .Property(e => e.LgAccount)
                .IsUnicode(false);

                modelBuilder.Entity<AdminUser>()
                    .Property(e => e.Email)
                    .IsUnicode(false);

                modelBuilder.Entity<AdminUser>()
                   .Property(e => e.PasswordHash)
                   .IsUnicode(false);

                modelBuilder.Entity<AdminUser>()
                   .Property(e => e.Gender)
                   .IsUnicode(false);

                modelBuilder.Entity<AdminUser>()
                    .Property(e => e.Phone)
                    .IsUnicode(false);
            });
        }

        
    }
}
