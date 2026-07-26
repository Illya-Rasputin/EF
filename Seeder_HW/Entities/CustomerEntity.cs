using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seeder_HW.Entities
{
    public class CustomerEntity
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        // One-to-Many
        public virtual List<OrderEntity> Orders { get; set; } = new List<OrderEntity>();
    }
}
