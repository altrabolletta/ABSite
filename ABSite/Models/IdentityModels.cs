using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ABSite.Models
{
    // È possibile aggiungere dati del profilo per l'utente aggiungendo altre proprietà alla classe ApplicationUser. Per altre informazioni, vedere https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser<long, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Boolean IsUserActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastPassUpdate { get; set; }
        public DateTime LastLogin { get; set; }


        public ApplicationUser()
        {
            CreatedDate = DateTime.Now;
            LastPassUpdate = DateTime.Now;
            LastLogin = DateTime.Now;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
        {
            // Tenere presente che il valore di authenticationType deve corrispondere a quello definito in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Aggiungere qui i reclami utente personalizzati
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, long, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        //public ApplicationDbContext()
        //    : base("DefaultConnection", throwIfV1Schema: false)
        //{
        //}

        public ApplicationDbContext() : base("DefaultConnection") { }

        //public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            
            /*
            List<string> ltab3 = new List<string>();
            List<string> ltab4 = new List<string>();
            foreach (var entity in builder.Model.GetEntityTypes())
            {
                ltab3.Add(entity.Relational().TableName);
                ltab4.Add(entity.ToString());
            }*/
            modelBuilder.HasDefaultSchema("usr");

            //or configure all DateTime Preperties globally(EF 6 and Above)
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));

            modelBuilder.Entity<ApplicationUserLogin>().Map(c =>
            {
                c.ToTable("UserLogin");
                c.Properties(p => new
                {
                    p.UserId,
                    p.LoginProvider,
                    p.ProviderKey
                });
            }).HasKey(p => new { p.LoginProvider, p.ProviderKey, p.UserId });

            // Mapping for ApiRole
            modelBuilder.Entity<ApplicationRole>().Map(c =>
            {
                c.ToTable("Role");
                c.Property(p => p.Id).HasColumnName("RoleId");
                c.Properties(p => new
                {
                    p.Name,
                    p.Description,
                    p.CreatedDate
                });
            }).HasKey(p => p.Id);
            modelBuilder.Entity<ApplicationRole>().HasMany(c => c.Users).WithRequired().HasForeignKey(c => c.RoleId);

            modelBuilder.Entity<ApplicationUser>().Map(c =>
            {
                c.ToTable("User");
                c.Property(p => p.Id).HasColumnName("UserId");
                c.Properties(p => new
                {
                    p.AccessFailedCount,
                    p.Email,
                    p.EmailConfirmed,
                    p.PasswordHash,
                    p.PhoneNumber,
                    p.PhoneNumberConfirmed,
                    p.TwoFactorEnabled,
                    p.SecurityStamp,
                    p.LockoutEnabled,
                    p.LockoutEndDateUtc,
                    p.UserName,
                    p.FirstName,
                    p.LastName,
                    p.IsUserActive,
                    p.CreatedDate,
                    p.LastPassUpdate,
                    p.LastLogin
                });
            }).HasKey(c => c.Id);
            modelBuilder.Entity<ApplicationUser>().HasMany(c => c.Logins).WithOptional().HasForeignKey(c => c.UserId);
            modelBuilder.Entity<ApplicationUser>().HasMany(c => c.Claims).WithOptional().HasForeignKey(c => c.UserId);
            modelBuilder.Entity<ApplicationUser>().HasMany(c => c.Roles).WithRequired().HasForeignKey(c => c.UserId);

            modelBuilder.Entity<ApplicationUserRole>().Map(c =>
            {
                c.ToTable("UserRole");
                c.Properties(p => new
                {
                    p.UserId,
                    p.RoleId
                });
            })
            .HasKey(c => new { c.UserId, c.RoleId });

            modelBuilder.Entity<ApplicationUserClaim>().Map(c =>
            {
                c.ToTable("UserClaim");
                c.Property(p => p.Id).HasColumnName("UserClaimId");
                c.Properties(p => new
                {
                    p.UserId,
                    p.ClaimValue,
                    p.ClaimType
                });
            }).HasKey(c => c.Id);

        }
    }
}