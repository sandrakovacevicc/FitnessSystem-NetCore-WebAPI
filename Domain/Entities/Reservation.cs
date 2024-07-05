using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public string ClientJMBG { get; set; }
        public int SessionId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Status { get; set; }
        public Client Client { get; set; }
        public Session Session { get; set; }

    }
}
