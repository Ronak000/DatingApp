using API.Extensions;
using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public DateTime DateOfBirth { get; set; } 
        public string KnownAs { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime LastActive { get; set; } = DateTime.UtcNow;
        public string Gender { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public List<Photo> Photos { get; set; } = new();
        public ICollection<AppUserRole> UserRoles { get; set; }

        public int GetAge()
        {
            var today = (DateTime.UtcNow); // 05/01/2022 2:12

            var age = today.Year - this.DateOfBirth.Year; // 2022  -  2000== 22

            if(this.DateOfBirth.Date > today.AddYears(-age)) age--; // 04/01/2022 >  05/01/2000

            return age;
        }
    }
}