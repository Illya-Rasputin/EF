using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seeder_HW.Entities
{
    public class OrderItemEntity
    {
        public int Id { get; set; }

        // Foreign Keys
        public int OrderId { get; set; }

        public int GameId { get; set; }

        public int Quantity { get; set; }

        // Navigation Properties
        public virtual OrderEntity? Order { get; set; } = null!;

        public virtual GameEntity? Game { get; set; } = null!;
    }
}

