using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Airportproject1.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public int FlightNumber { get; set; }
        public string? Name { get; set; }
        public bool isDeparting { get; set; }
        [JsonIgnore]
        [NotMapped]
        public virtual Station? CurrentStation { get; set; }
    }
}