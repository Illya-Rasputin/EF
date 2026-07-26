using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seeder_HW.Entities
{
    public class GameEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Column(TypeName = "decimal(10,2)")]
        public double Price { get; set; }

        public int ReleaseYear { get; set; }

        
        public int DeveloperId { get; set; }

        
        public virtual DeveloperEntity? Developer { get; set; } = null!;

        
        public virtual List<OrderItemEntity> OrderItems { get; set; } = new List<OrderItemEntity>();


        public override string ToString()
        {
            return $"{Id,-2} {Name,-40} ({ReleaseYear}) - ${Price:F2} | Dev: {Developer?.Name}";
        }
    }
}
