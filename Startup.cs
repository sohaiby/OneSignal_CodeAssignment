using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using OneSignal.Models;
using Owin;

[assembly: OwinStartupAttribute(typeof(OneSignal.Startup))]
namespace OneSignal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndUsers();
        }

        // In this method we will create default User roles and Admin user for login    
        private void CreateRolesAndUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // Creating Admin Role and a default Admin User     
            if (!roleManager.RoleExists("Admin"))
            {

                // Creating Admin role    
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "admin";
                user.Email = "admin@gmail.com";
                user.EmailConfirmed = true;

                string pwd = "admin123";

                var chkUser = UserManager.Create(user, pwd);

                //Add default User to Role Admin    
                if (chkUser.Succeeded)
                {
                    var result = UserManager.AddToRole(user.Id, "Admin");

                }
            }

            // Creating Data Entry Operator role     
            if (!roleManager.RoleExists("DataEntryOperator"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "DataEntryOperator";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "operator";
                user.Email = "operator@gmail.com";
                user.EmailConfirmed = true;

                string pwd = "operator123";

                var chkUser = UserManager.Create(user, pwd);

                if (chkUser.Succeeded)
                {
                    var result2 = UserManager.AddToRole(user.Id, "DataEntryOperator");

                }

            }
        }
    }
}
