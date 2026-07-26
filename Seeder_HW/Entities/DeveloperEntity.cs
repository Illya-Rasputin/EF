using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seeder_HW.Entities
{
    public class DeveloperEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Country { get; set; } = null!;

        // One-to-Many
        public virtual List<GameEntity> Games { get; set; } = new List<GameEntity>();
    }
}
