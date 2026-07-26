using Microsoft.EntityFrameworkCore;
using Seeder_HW.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Seeder_HW.Data
{
    public class Seeder
    {
        public static void Seed(AppDbContext context)
        {
            context.Database.Migrate();

            SeedDevAndGame(context);
            SeedCustomersAndOrders(context);
        }
        private class OrderSeed
        {
            public string CustomerEmail { get; set; } = null!;
            public string OrderDate { get; set; } = DateTime.UtcNow.ToString("o");
            public List<OrderItemSeed>? Items { get; set; }
        }

        private class OrderItemSeed
        {
            public string GameName { get; set; } = null!;
            public int Quantity { get; set; }
        }
        private static void SeedDevAndGame(AppDbContext context)
        {
            var dataPath = @"C:\Users\cride\source\repos\EF\Seeder_HW\Data\SeedData";
            var devsPath = Path.Combine(dataPath, "developers.json");
            var gamesPage = Path.Combine(dataPath, "Games");

            if (!File.Exists(devsPath))
                return;

            var devsjson = File.ReadAllText(devsPath);
            var devs = JsonSerializer.Deserialize<List<DeveloperEntity>>(devsjson);

            if (devs == null)
                return;

            foreach (var item in devs)
            {
                var dev = item;

                if (!context.Developers.Any(d => d.Name == dev.Name))
                {
                    context.Developers.Add(dev);
                }
                else
                {
                    dev = context.Developers.FirstOrDefault(d => d.Name == item.Name)!;
                }

                if (!dev.Games.Any())
                {
                    var path = Path.Combine(gamesPage, $"{dev.Name}.json");

                    if (File.Exists(path))
                    {
                        var productsJson = File.ReadAllText(path);
                        var products = JsonSerializer.Deserialize<List<GameEntity>>(productsJson);

                        if (products != null)
                        {
                            dev.Games.AddRange(products);
                        }
                    }
                }
            }

            context.SaveChanges();
        }

        private static void SeedCustomersAndOrders(AppDbContext context)
        {
            var dataPath = @"C:\Users\cride\source\repos\EF\Seeder_HW\Data\SeedData";
            var customersPath = Path.Combine(dataPath, "customers.json");
            var ordersPath = Path.Combine(dataPath, "orders.json");


            // 1) Seed customers
            if (File.Exists(customersPath))
            {
                var customersJson = File.ReadAllText(customersPath);
                var customers = JsonSerializer.Deserialize<List<CustomerEntity>>(customersJson);
                if (customers != null)
                {
                    foreach (var c in customers)
                    {
                        if (!context.Customers.Any(x => x.Email == c.Email))
                        {
                            context.Customers.Add(c);
                        }
                    }
                    context.SaveChanges();
                }
            }

            // 2) Seed orders + order items, matching games from DB first, then from files in products folder
            if (!File.Exists(ordersPath))
                return;

            var ordersJson = File.ReadAllText(ordersPath);
            var orderSeeds = JsonSerializer.Deserialize<List<OrderSeed>>(ordersJson);
            if (orderSeeds == null) return;

            foreach (var seed in orderSeeds)
            {
                var customer = context.Customers.FirstOrDefault(c => c.Email == seed.CustomerEmail)
                           ?? context.Customers.FirstOrDefault(c => c.FullName == seed.CustomerEmail);

                if (customer == null)
                    continue;

                var parsedDate = DateTime.TryParse(seed.OrderDate, out var dt) ? dt : DateTime.UtcNow;
                if (context.Orders.Any(o => o.CustomerId == customer.Id && o.OrderDate == parsedDate))
                    continue;

                var order = new OrderEntity
                {
                    CustomerId = customer.Id,
                    OrderDate = parsedDate
                };

                foreach (var it in seed.Items ?? Enumerable.Empty<OrderItemSeed>())
                {
                    var game = context.Games.FirstOrDefault(g => g.Name == it.GameName)
                               ?? context.Games.FirstOrDefault(g => g.Name != null && g.Name.Contains(it.GameName, StringComparison.OrdinalIgnoreCase));

                    

                    if (game == null)
                        continue;

                    var orderItem = new OrderItemEntity
                    {
                        GameId = game.Id,
                        Quantity = it.Quantity
                    };

                    order.OrderItems.Add(orderItem);
                }

                if (order.OrderItems.Any())
                {
                    context.Orders.Add(order);
                }
            }

            context.SaveChanges();
        }
        
    }
}