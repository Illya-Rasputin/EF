using Intro_2.Entities;
using System.Text;

namespace Intro_2
{
    internal class Program
    {
        static void ReadTable(TeamDbContext context)
        {
            // Виведення
            IQueryable<TeamEntity> teams = context.Teams
                .OrderBy(g => g.Name);

            foreach (var team in teams)
            {
                Console.WriteLine("---------------------------");
                Console.WriteLine(team);
                Console.WriteLine("---------------------------");
            }
        }

        static void AddNewData(TeamDbContext context)
        {
            var teams = new TeamEntity[]
            {
                new TeamEntity
                {
                    Name = "Real Madrid",
                    City = "Madrid",
                    Wins = 29,
                    Losses = 4,
                    Draws = 5
                },
                new TeamEntity
                {
                    Name = "Barcelona",
                    City = "Barcelona",
                    Wins = 27,
                    Losses = 6,
                    Draws = 5
                },
                new TeamEntity
                {
                    Name = "Atlético Madrid",
                    City = "Madrid",
                    Wins = 22,
                    Losses = 8,
                    Draws = 8
                }
            };
            context.Teams.AddRange(teams);
            context.SaveChanges();
        }
        static void UpdateData(TeamDbContext context)
        {

            var updatedProduct = context.Teams.FirstOrDefault(g => g.Id == 1);

            if (updatedProduct != null)
            {
                updatedProduct.Scores = 10;
                updatedProduct.ScoredOn = 5;
                context.Teams.Update(updatedProduct);
                context.SaveChanges();
            }
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            using var context = new TeamDbContext();

            UpdateData(context);
            //AddNewData(context);
            ReadTable(context);
        }
    }
}
