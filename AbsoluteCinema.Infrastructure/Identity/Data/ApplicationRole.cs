using Microsoft.AspNetCore.Identity;

namespace AbsoluteCinema.Infrastructure.Identity.Data
{
    public class ApplicationRole : IdentityRole<int>
    {
        public ApplicationRole() : base() { }
    }
}
