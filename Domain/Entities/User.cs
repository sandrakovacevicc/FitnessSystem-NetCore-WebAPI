
using Microsoft.AspNetCore.Identity;

namespace Core.Entities

{
    public class User : IdentityUser
    {
        public string JMBG { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }


    }
}
