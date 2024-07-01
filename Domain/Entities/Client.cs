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
        public List<Session> Sessions { get; set; }

    }
}
