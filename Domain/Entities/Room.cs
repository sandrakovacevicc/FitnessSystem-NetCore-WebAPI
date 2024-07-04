using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Room
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        [JsonIgnore]
        public Session Session { get; set; }

    }
}
