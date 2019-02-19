using System;

namespace Common.Helpers.Models
{
    public class Entity
    {
        public long Id { get; set; }
        public DateTime CreateOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}
