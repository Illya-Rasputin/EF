using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intro_2.Entities
{
    public class TeamEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
        public string City { get; set; }
        public int Wins { get; set; } = 0;
        public int Losses { get; set; } = 0;
        public int Draws { get; set; } = 0;
        public int Scores { get; set; } = 0;
        public int ScoredOn { get; set; } = 0;

        public override string ToString()
        {
            return $"{Id}: {Name}\nFrom: {City}\nRecord(W-L-D): {Wins}-{Losses}-{Draws}\nScores: {Scores}-{ScoredOn}";
        }
    }   
}
