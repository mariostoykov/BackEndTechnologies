using System;
using System.IO;
using System.Collections.Generic;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

public class Order
{
    public int order_id { get; set; }
    public string customer { get; set; }
    public string item { get; set; }
    public int quantity { get; set; }
    public decimal total_amount { get; set; }
}

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Which file would you like to use?");
        string fileName = Console.ReadLine();

        string path = Path.Combine("Datasets", fileName + ".yaml");

        if (!File.Exists(path))
        {
            Console.WriteLine("File not found.");
            return;
        }

        try
        {
            string yaml = File.ReadAllText(path);

            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(UnderscoredNamingConvention.Instance)
                .Build();

            List<Order> orders = deserializer.Deserialize<List<Order>>(yaml);

            Console.WriteLine("\nParsed Orders:\n");

            foreach (var order in orders)
            {
                Console.WriteLine($"Order ID: {order.order_id}");
                Console.WriteLine($"Customer: {order.customer}");
                Console.WriteLine($"Item: {order.item}");
                Console.WriteLine($"Quantity: {order.quantity}");
                Console.WriteLine($"Total Amount: {order.total_amount}");
                Console.WriteLine(new string('-', 40));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error parsing YAML:");
            Console.WriteLine(ex.Message);
        }
    }
}