using System;
using System.IO;
using System.Collections.Generic;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

public class Service
{
    public string type { get; set; }
    public string date { get; set; }
    public string time { get; set; }
}

public class Reservation
{
    public int reservation_id { get; set; }
    public string guest_name { get; set; }
    public List<Service> services { get; set; }
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

            List<Reservation> reservations =
                deserializer.Deserialize<List<Reservation>>(yaml);

            Console.WriteLine("\nParsed Reservations:\n");

            foreach (var reservation in reservations)
            {
                Console.WriteLine($"Reservation ID: {reservation.reservation_id}");
                Console.WriteLine($"Guest: {reservation.guest_name}");
                Console.WriteLine("Services:");

                foreach (var service in reservation.services)
                {
                    Console.WriteLine($" - Type: {service.type}");
                    Console.WriteLine($"   Date: {service.date}");
                    Console.WriteLine($"   Time: {service.time}");
                }

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