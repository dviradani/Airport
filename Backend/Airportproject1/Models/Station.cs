using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Airportproject1.Models
{
    public class Station
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        [NotMapped]
        public virtual Flight? Flight { get; set; } = null;
        [JsonIgnore]
        public int FlightId { get; set; }
        [NotMapped]
        public virtual Queue<Flight> Queue { get; set; } = new Queue<Flight>();


    }
}
