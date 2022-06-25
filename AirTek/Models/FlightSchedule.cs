using System.Collections.Generic;

namespace AirTek.Models
{
    class FlightSchedule
    {
        public int FlightNumber { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }
        public int Day { get; set; }
        public List<string> OrdersList { get; set; }
    }
}
