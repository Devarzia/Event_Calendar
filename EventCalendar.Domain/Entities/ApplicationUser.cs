using Microsoft.AspNetCore.Identity;

namespace EventCalendar.Domain
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] ProfilePicture { get; set; }
    }

    public class ApplicationUserLogin : IdentityUserLogin<int> { }
    public class ApplicationUserRole : IdentityUserRole<int> { }
    public class ApplicationUserClaim : IdentityUserClaim<int> { }
    public class ApplicationRole : IdentityRole<int> { }
    public class ApplicationRoleClaim : IdentityRoleClaim<int> { }
    public class ApplicationUserToken : IdentityUserToken<int> { }
}
