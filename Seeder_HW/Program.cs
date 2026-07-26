using Seeder_HW.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Seeder_HW
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            using var context = new AppDbContext();
            //Seeder.Seed(context);

            //PrintGames(context);
            //PrintOrders(context);
            //OrderTotal(context);
            Top3(context);
            MoreThanOneOrder(context);
            TotalRevenue(context);
        }

        static void PrintGames(AppDbContext context)
        {
            var games = context.Games.Include(g => g.Developer).ToList();
            foreach (var g in games)
            {
                Console.WriteLine(g);
            }
            Console.WriteLine();
        }

        static void PrintOrders(AppDbContext context)
        {
            var orders = context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems).ThenInclude(oi => oi.Game)
                .ToList();

            foreach (var o in orders)
            {
                Console.WriteLine($"Order #{o.Id} from {o.OrderDate:d} - Customer: {o.Customer?.FullName}");
                foreach (var oi in o.OrderItems)
                {
                    double itemTotal = (oi.Game != null ? oi.Game.Price : 0) * oi.Quantity;
                    Console.WriteLine($"  • {oi.Game?.Name} x{oi.Quantity} @ {oi.Game?.Price:F2} = ${itemTotal:F2}");
                }
            }
            Console.WriteLine();
        }

        static void OrderTotal(AppDbContext context)
        {
            var totals = context.Orders
                .Include(o => o.OrderItems).ThenInclude(oi => oi.Game)
                .Select(o => new
                {
                    OrderId = o.Id,
                    Total = o.OrderItems.Sum(oi => (oi.Game != null ? oi.Game.Price : 0) * oi.Quantity)
                })
                .ToList();

            foreach (var t in totals)
            {
                Console.WriteLine($"Order #{t.OrderId}: ${t.Total:F2}.");
            }
            Console.WriteLine();
        }

        static void Top3(AppDbContext context)
        {
            var top3 = context.Games
                .OrderByDescending(g => g.Price)
                .Take(3)
                .Include(g => g.Developer)
                .ToList();

            int rank = 1;
            foreach (var g in top3)
            {
                Console.WriteLine($"{rank}. " + g);
                rank++;
            }
            Console.WriteLine();
        }

        static void MoreThanOneOrder(AppDbContext context)
        {
            var customers = context.Customers
                .Include(c => c.Orders)
                .Where(c => c.Orders.Count > 1)
                .ToList();

            foreach (var c in customers)
            {
                Console.WriteLine($"- {c.FullName} ({c.Email}) - Orders: {c.Orders.Count}");
            }
            Console.WriteLine();
        }

        static void TotalRevenue(AppDbContext context)
        {
            var revenue = context.OrderItems
                .Include(oi => oi.Game)
                .AsEnumerable()
                .Sum(oi => (oi.Game != null ? oi.Game.Price : 0) * oi.Quantity);

            Console.WriteLine($"Total Revenue: ${revenue:F2}.");
            Console.WriteLine();
        }
    }
}
