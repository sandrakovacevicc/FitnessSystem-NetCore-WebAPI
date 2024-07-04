using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Trainer : User
    {
        public string Specialty { get; set; }
        [JsonIgnore]
        public List<Session?>? Sessions { get; set; }

    }
}
