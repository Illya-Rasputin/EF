using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intro_1.Entities
{
    public class GameEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
        public string PlayMode { get; set; } = "Single Player";
        public string Developer { get; set; }
        public DateTime ReleaseDate { get; set; } = DateTime.Now;
        public int Copies { get; set; }


        public override string ToString()
        {
            return $"{Id}: {Name}\nPlay Mode: {PlayMode}\nDeveloper: {Developer}\nRelease Date: {ReleaseDate}\nCopies: {Copies}";
        }
    }
}
