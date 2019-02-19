using System;

namespace Common.Messaging.Models
{
    public class PingVehicleModel 
    {
        public Guid Id { get; set; }
        public string VehicleId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}