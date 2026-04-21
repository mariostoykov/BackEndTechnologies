using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("devices")]
public class Devices
{
    [XmlElement("device")]
    public List<Device> DeviceList { get; set; }
}

public class Device
{
    public string type { get; set; }
    public string brand { get; set; }
    public string specs { get; set; }
    public decimal price { get; set; }
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
            XmlSerializer serializer = new XmlSerializer(typeof(Devices));

            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                Devices devices = (Devices)serializer.Deserialize(fs);

                Console.WriteLine("\nParsed Devices:\n");

                foreach (var device in devices.DeviceList)
                {
                    Console.WriteLine($"Type: {device.type}");
                    Console.WriteLine($"Brand: {device.brand}");
                    Console.WriteLine($"Specs: {device.specs}");
                    Console.WriteLine($"Price: {device.price}");
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