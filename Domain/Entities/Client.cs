using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Client : User
    {
        public int MembershipPackageId { get; set; }
        public MembershipPackage MembershipPackage { get; set; }
        public DateTime Birthdate { get; set; }
        public string MobileNumber { get; set; }
        [JsonIgnore]
        public List<Session> Sessions { get; set; }

    }
}
