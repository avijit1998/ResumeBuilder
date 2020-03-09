using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ResumeBuilder.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Skills> Skills { get; set; }
        public DbSet<Languages> Languages { get; set; }
        public DbSet<StudentsRegistration> StudentsRegistration { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UsersSkill> UsersSkills { get; set; }
        public DbSet<UsersLanguage> UsersLanguage { get; set; }
        public DbSet<EducationalDetails> EducationalDetails { get; set; }

        public DbSet<LoginViewModel> Logins { get; set; }
        public DbSet<RegisterViewModel> Registers { get; set; } 

        public ApplicationDbContext()
            : base("ResumeBuilderConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {

            return new ApplicationDbContext();
        }
    }
}