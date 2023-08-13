using DocumentSign_Models.TableModels;
using DocumentSign_Models.ViewModels;
using System.Data.Entity;

namespace DocumentSign_Models
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("name=DatabaseContext")
        {
        }

        public virtual DbSet<ErrorLog> ErrorLogs { get; set; }
        public virtual DbSet<ModuleRole> ModuleRoles { get; set; }
        public virtual DbSet<ModuleUserRole> ModuleUserRoles { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<UserDepartment> UserDepartments { get; set; }

        public virtual DbSet<vwAccountsApproval> vwAccountsApprovals { get; set; }
        public virtual DbSet<vwAspNetRole> vwAspNetRoles { get; set; }
        public virtual DbSet<vwAspNetUserRole> vwAspNetUserRoles { get; set; }
        public virtual DbSet<vwAspNetUser> vwAspNetUsers { get; set; }
        public virtual DbSet<VwUserProfile> VwUserProfiles { get; set; }
        public virtual DbSet<vwRequisition> vwRequisitions { get; set; }
        public virtual DbSet<vwRequisitionDocument> vwRequisitionDocuments { get; set; }
        public virtual DbSet<vwRequisitionTemplate> vwRequisitionTemplates { get; set; }
        public virtual DbSet<VwDocument> VwDocuments { get; set; }
        public virtual DbSet<VwUserDepartment> VwUserDepartments { get; set; }
        public virtual DbSet<VwDepartment> VwDepartment { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserRoles)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);
        }
    }
}
