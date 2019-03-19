using System;

namespace Common.Messaging.Models
{
    public class UserCreatedMessage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}