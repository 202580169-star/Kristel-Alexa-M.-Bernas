using System;
using System.Collections.Generic; 

namespace Problem2
{
    struct Student
    {
        public string StudentNumber;
        public string Name;
        public string Program;
        public int YearLevel;
    }

    class Program
    {
        const int MaxStudents = 10;
        static Student[] students = new Student[MaxStudents]; 
        static int studentCount = 0;

        static Dictionary<string, Student> studentDictionary = new Dictionary<string, Student>();

        static void Main()
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n========================================");
                Console.WriteLine("        STUDENT LOOKUP USING DICTIONARY");
                Console.WriteLine("========================================\n");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Search Student");
                Console.WriteLine("3. Display All Students");
                Console.WriteLine("4. Exit\n");
                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddStudent(); break;
                    case "2": SearchStudent(); break;
                    case "3": DisplayAll(); break;
                    case "4":
                        Console.WriteLine("\nProgram exited.");
                        running = false;
                        break;
                    default:
                        Console.WriteLine("\nInvalid choice. Please enter 1-4.");
                        break;
                }
            }
        }

        static void AddStudent()
        {
            if (studentCount >= MaxStudents)
            {
                Console.WriteLine("\nCannot add more students. The list is full (10 max).");
                return;
            }

            Console.Write("\nEnter Student Number: ");
            string number = Console.ReadLine();

            if (studentDictionary.ContainsKey(number))
            {
                Console.WriteLine("Student Number already exists. Duplicates are not allowed.");
                return;
            }

            Student s = new Student();
            s.StudentNumber = number;
            Console.Write("Enter Name: ");
            s.Name = Console.ReadLine();
            Console.Write("Enter Program: ");
            s.Program = Console.ReadLine();
            s.YearLevel = ReadYearLevel();

            students[studentCount] = s;          
            studentCount++;
            studentDictionary.Add(number, s);    

            Console.WriteLine("\nStudent added successfully!");
        }

        static void SearchStudent()
        {
            Console.Write("\nEnter Student Number to search: ");
            string number = Console.ReadLine();

            if (studentDictionary.TryGetValue(number, out Student found))
            {
                Console.WriteLine("\nStudent Found!");
                Console.WriteLine("Student Number: " + found.StudentNumber);
                Console.WriteLine("Name: " + found.Name);
                Console.WriteLine("Program: " + found.Program);
                Console.WriteLine("Year Level: " + found.YearLevel);
            }
            else
            {
                Console.WriteLine("\nStudent not found. That Student Number does not exist.");
            }
        }

        static void DisplayAll()
        {
            if (studentCount == 0)
            {
                Console.WriteLine("\nNo student records to display.");
                return;
            }

            Console.WriteLine("\n========================================");
            Console.WriteLine("            STUDENT RECORDS");
            Console.WriteLine("========================================");

            for (int i = 0; i < studentCount; i++)
            {
                Console.WriteLine("Student Number: " + students[i].StudentNumber);
                Console.WriteLine("Name: " + students[i].Name);
                Console.WriteLine("Program: " + students[i].Program);
                Console.WriteLine("Year Level: " + students[i].YearLevel);
                Console.WriteLine();
            }
        }

        static int ReadYearLevel()
        {
            while (true)
            {
                Console.Write("Enter Year Level (1-4): ");
                if (int.TryParse(Console.ReadLine(), out int year) && year >= 1 && year <= 4)
                    return year;

                Console.WriteLine("Invalid year level. Please enter 1, 2, 3, or 4.");
            }
        }
    }
}
