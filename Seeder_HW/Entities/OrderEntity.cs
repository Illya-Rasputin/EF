using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seeder_HW.Entities
{
    public class OrderEntity
    {
        public int Id { get; set; }

        // Foreign Key
        public int CustomerId { get; set; }

        public DateTime OrderDate { get; set; }

        // Navigation Property
        public virtual CustomerEntity? Customer { get; set; } = null!;

        // Many-to-Many (through OrderItem)
        public virtual List<OrderItemEntity> OrderItems { get; set; } = new List<OrderItemEntity>();
    }
}
