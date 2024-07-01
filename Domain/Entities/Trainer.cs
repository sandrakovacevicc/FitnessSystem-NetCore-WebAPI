using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Trainer : User
    {
        public string Specialty { get; set; }
        public List<Session> Sessions { get; set; }

    }
}
