using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("vehicles")]
public class Vehicles
{
    [XmlElement("vehicle")]
    public List<Vehicle> VehicleList { get; set; }
}

public class Vehicle
{
    public string type { get; set; }
    public string model { get; set; }
    public string specs { get; set; }
    public string color { get; set; }
}

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Which file would you like to use?");
        string fileName = Console.ReadLine();

        string path = Path.Combine("Datasets", fileName + ".xml");

        if (!File.Exists(path))
        {
            Console.WriteLine("File not found.");
            return;
        }

        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Vehicles));

            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                Vehicles vehicles = (Vehicles)serializer.Deserialize(fs);

                Console.WriteLine("\nParsed Vehicles:\n");

                foreach (var vehicle in vehicles.VehicleList)
                {
                    Console.WriteLine($"Type: {vehicle.type}");
                    Console.WriteLine($"Model: {vehicle.model}");
                    Console.WriteLine($"Specs: {vehicle.specs}");
                    Console.WriteLine($"Color: {vehicle.color}");
                    Console.WriteLine(new string('-', 40));
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error parsing XML:");
            Console.WriteLine(ex.Message);
        }
    }
}