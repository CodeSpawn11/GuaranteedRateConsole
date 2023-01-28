using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.InteropServices;

public class Customer
{
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string Email { get; set; }
    public string FavoriteColor { get; set; }
    public DateTime DateOfBirth { get; set; }

    public Customer(string lastName, string firstName, string email, string favoriteColor, DateTime dateOfBirth)
    {
        LastName = lastName;
        FirstName = firstName;
        Email = email;
        FavoriteColor = favoriteColor;
        DateOfBirth = dateOfBirth;
    }
}

class Program
{
    static void Main(string[] args)
    {

        #region Program Instructions
        //This code uses the `File.ReadAllLines` method to read in the contents of each file, and the `Select`
        //and `Concat` methods to parse and combine the records into a single list. It then uses Linq's `OrderBy`, `ThenBy`, and `OrderByDescending` methods to sort
        //the records in the three different ways specified in the output. The date format is also formatted as "M/d/yyyy" as required.  Please note that you should 
        //change the file name in the File.ReadAllLines to the name of your file
        #endregion


        // This will get the current WORKING directory (i.e. \bin\Debug)
        string workingDirectory = Environment.CurrentDirectory;

        // This will get the current PROJECT directory
        string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

        string path = projectDirectory + @"\Tests\pipe-delimited.txt";
        var pipeDelimitedRecords = File.ReadAllLines(path)
            .Select(x => x.Split('|'))
            .Select(x => new Customer(x[0], x[1], x[2], x[3], DateTime.Parse(x[4])));

        // Parse the comma-delimited file
        path = projectDirectory + @"\Tests\comma-delimited.txt";
        var commaDelimitedRecords = File.ReadAllLines(path)
            .Select(x => x.Split(','))
            .Select(x => new Customer(x[0], x[1], x[2], x[3], DateTime.Parse(x[4])));

        // Parse the space-delimited file
        path = projectDirectory + @"\Tests\space-delimited.txt";
        var spaceDelimitedRecords = File.ReadAllLines(path)
            .Select(x => x.Split(' '))
            .Select(x => new Customer(x[0], x[1], x[2], x[3], DateTime.Parse(x[4])));

        // Combine all records into a single set
        var records = pipeDelimitedRecords.Concat(commaDelimitedRecords).Concat(spaceDelimitedRecords).ToList();

        // Output 1 - sort by favorite color then by last name ascending
        Console.WriteLine("Output 1 - sorted by favorite color then by last name ascending:");
        var output1 = records.OrderBy(x => x.FavoriteColor).ThenBy(x => x.LastName);
        foreach (var record in output1)
        {
            Console.WriteLine($"{record.LastName}, {record.FirstName}, {record.Email}, {record.FavoriteColor}, {record.DateOfBirth.ToString("M/d/yyyy")}");
        }

        // Output 2 - sort by birth date, ascending
        Console.WriteLine("Output 2 - sorted by birth date, ascending:");
        var output2 = records.OrderBy(x => x.DateOfBirth);
        foreach (var record in output2)
        {
            Console.WriteLine($"{record.LastName}, {record.FirstName}, {record.Email}, {record.FavoriteColor}, {record.DateOfBirth.ToString("M/d/yyyy")}");
        }

        // Output 3 - sort by last name, descending
        Console.WriteLine("Output 3 - sorted by last name, descending:");
        var output3 = records.OrderByDescending(x => x.LastName);
        foreach (var record in output3)
        {
            Console.WriteLine($"{record.LastName}, {record.FirstName}, {record.Email}, {record.FavoriteColor}, {record.DateOfBirth.ToString("M/d/yyyy")}");
        }

        Console.ReadLine();
    }

}