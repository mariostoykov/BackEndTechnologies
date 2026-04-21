using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

public class Course
{
    public string name { get; set; }
}

public class Student
{
    public string name { get; set; }
    public int age { get; set; }
    public List<Course> courses { get; set; }
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

            List<Student> students =
                JsonSerializer.Deserialize<List<Student>>(json);

            Console.WriteLine("\nParsed Students:\n");

            foreach (var student in students)
            {
                Console.WriteLine($"Name: {student.name}");
                Console.WriteLine($"Age: {student.age}");
                Console.WriteLine("Courses:");

                foreach (var course in student.courses)
                {
                    Console.WriteLine($" - {course.name}");
                }

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