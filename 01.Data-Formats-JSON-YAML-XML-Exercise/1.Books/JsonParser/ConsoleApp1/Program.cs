using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

public class Book
{
    public string title { get; set; }
    public string author { get; set; }
    public int released { get; set; }
    public int pages { get; set; }
    public string ISBN { get; set; }
}

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Which file would you like to use?");
        string fileName = Console.ReadLine();

        string path = Path.Combine("Datasets", fileName + ".json");

        if (!File.Exists(path))
        {
            Console.WriteLine("File not found.");
            return;
        }

        try
        {
            string json = File.ReadAllText(path);

            List<Book> books = JsonSerializer.Deserialize<List<Book>>(json);

            Console.WriteLine("\nParsed Books:\n");

            foreach (var book in books)
            {
                Console.WriteLine($"Title: {book.title}");
                Console.WriteLine($"Author: {book.author}");
                Console.WriteLine($"Released: {book.released}");
                Console.WriteLine($"Pages: {book.pages}");
                Console.WriteLine($"ISBN: {book.ISBN}");
                Console.WriteLine(new string('-', 40));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error parsing JSON:");
            Console.WriteLine(ex.Message);
        }
    }
}