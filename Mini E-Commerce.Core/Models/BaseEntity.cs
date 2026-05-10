using System;
using System.Collections.Generic;
using System.Text;

namespace Mini_E_Commerce.Core.Models
{
    public class BaseEntity
    {
        public Guid Id { get; set; } 
        public bool  IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }= DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        
    
    }
}
