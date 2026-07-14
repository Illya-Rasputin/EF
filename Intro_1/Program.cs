using Intro_1.Entities;
using System.Text;

namespace Intro_1
{
    internal class Program
    {
        static void ReadTable(GameDbContext context)
        {
            // Виведення
            IQueryable<GameEntity> games = context.Games
                //.Where(g => g.ReleaseDate.Year == 2020)
                .OrderBy(g => g.Name);

            foreach (var game in games)
            {
                Console.WriteLine("---------------------------");
                Console.WriteLine(game);
                Console.WriteLine("---------------------------");
            }
        }
        static void AddNewData(GameDbContext context)
        {

            var games = new GameEntity[]
            {
                new GameEntity
                {
                    Name = "Minecraft",
                    Developer = "Mojang Studios",
                    ReleaseDate = new DateTime(2011, 11, 18)
                },
                new GameEntity
                {
                    Name = "The Witcher 3: Wild Hunt",
                    Developer = "CD Projekt Red",
                    ReleaseDate = new DateTime(2015, 5, 19)
                },
                new GameEntity
                {
                    Name = "Grand Theft Auto V",
                    Developer = "Rockstar North",
                    ReleaseDate = new DateTime(2013, 9, 17)
                },
                new GameEntity
                {
                    Name = "Cyberpunk 2077",
                    Developer = "CD Projekt Red",
                    ReleaseDate = new DateTime(2020, 12, 10)
                }
            };

            context.Games.AddRange(games);


            context.SaveChanges();
        }
        static void UpdateData(GameDbContext context)
        {
            
            var updatedProduct = context.Games.FirstOrDefault(g => g.Id == 1);

            if (updatedProduct != null)
            {
                updatedProduct.PlayMode = "Both";
                updatedProduct.Copies = 3499;
                context.Games.Update(updatedProduct);
                context.SaveChanges();
            }
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            using var context = new GameDbContext();

            //UpdateData(context);
            ReadTable(context);
            //AddNewData(context);
        }
        
    }
}
