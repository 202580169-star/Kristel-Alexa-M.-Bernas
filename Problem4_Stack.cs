using System;
using System.Collections.Generic; 

namespace Problem4
{
    struct Student
    {
        public string StudentNumber;
        public string Name;
        public string Program;
        public int YearLevel;
    }

    struct Operation
    {
        public string Action;
        public string StudentNumber;
        public string StudentName;
    }

    class Program
    {
        const int MaxStudents = 10;
        static Student[] students = new Student[MaxStudents];
        static int studentCount = 0;

        static Stack<Operation> operationHistory = new Stack<Operation>();

        static void Main()
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n========================================");
                Console.WriteLine("   STUDENT RECORDS + OPERATION HISTORY");
                Console.WriteLine("========================================\n");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Display All Students");
                Console.WriteLine("3. Search Student");
                Console.WriteLine("4. Update Student");
                Console.WriteLine("5. Delete Student");
                Console.WriteLine("--- Operation History ---");
                Console.WriteLine("6. View Operation History");
                Console.WriteLine("7. View Last Operation");
                Console.WriteLine("8. Remove Last Operation");
                Console.WriteLine("9. Exit\n");
                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddStudent(); break;
                    case "2": DisplayAll(); break;
                    case "3": SearchStudent(); break;
                    case "4": UpdateStudent(); break;
                    case "5": DeleteStudent(); break;
                    case "6": ViewHistory(); break;
                    case "7": ViewLastOperation(); break;
                    case "8": RemoveLastOperation(); break;
                    case "9":
                        Console.WriteLine("\nProgram exited.");
                        running = false;
                        break;
                    default:
                        Console.WriteLine("\nInvalid choice. Please enter 1-9.");
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

            if (FindIndex(number) != -1)
            {
                Console.WriteLine("A student with that Student Number already exists.");
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

            RecordOperation("Added", s.StudentNumber, s.Name); 
            Console.WriteLine("\nStudent added successfully!");
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
                PrintStudent(students[i]);
                Console.WriteLine();
            }
        }

        static void SearchStudent()
        {
            Console.Write("\nEnter Student Number to search: ");
            int index = FindIndex(Console.ReadLine());

            if (index == -1)
            {
                Console.WriteLine("\nStudent not found.");
                return;
            }

            Console.WriteLine("\nStudent Found!");
            PrintStudent(students[index]);
        }

        static void UpdateStudent()
        {
            Console.Write("\nEnter Student Number to update: ");
            int index = FindIndex(Console.ReadLine());

            if (index == -1)
            {
                Console.WriteLine("\nStudent not found.");
                return;
            }

            Student s = students[index];
            Console.Write("Enter new Name: ");
            s.Name = Console.ReadLine();
            Console.Write("Enter new Program: ");
            s.Program = Console.ReadLine();
            s.YearLevel = ReadYearLevel();
            students[index] = s;

            RecordOperation("Updated", s.StudentNumber, s.Name);  
            Console.WriteLine("\nStudent updated successfully!");
        }

        static void DeleteStudent()
        {
            Console.Write("\nEnter Student Number to delete: ");
            int index = FindIndex(Console.ReadLine());

            if (index == -1)
            {
                Console.WriteLine("\nStudent not found.");
                return;
            }

            Student removed = students[index];

            for (int i = index; i < studentCount - 1; i++)
            {
                students[i] = students[i + 1];
            }
            students[studentCount - 1] = new Student();
            studentCount--;

            RecordOperation("Deleted", removed.StudentNumber, removed.Name);  
            Console.WriteLine("\nStudent deleted successfully!");
        }
		
        static void RecordOperation(string action, string number, string name)
        {
            Operation op = new Operation();
            op.Action = action;
            op.StudentNumber = number;
            op.StudentName = name;

            operationHistory.Push(op);
        }

        static void ViewHistory()
        {
            if (operationHistory.Count == 0)
            {
                Console.WriteLine("\nNo recorded operations.");
                return;
            }

            Console.WriteLine("\nOPERATION HISTORY\n");

            Operation[] items = operationHistory.ToArray();
            int number = 1;
            for (int i = items.Length - 1; i >= 0; i--)
            {
                Console.WriteLine(number + ". " + items[i].Action + " " + items[i].StudentName);
                number++;
            }
        }

        static void ViewLastOperation()
        {
            if (operationHistory.Count == 0)
            {
                Console.WriteLine("\nNo recorded operations.");
                return;
            }

            Operation last = operationHistory.Peek();
            Console.WriteLine("\nLast Operation: " + last.Action + " " + last.StudentName);
		}
        static void RemoveLastOperation()
        {
            if (operationHistory.Count == 0)
            {
                Console.WriteLine("\nNo recorded operations to remove.");
                return;
            }

            operationHistory.Pop();
            Console.WriteLine("\nLast operation removed successfully!");
        }

        static int FindIndex(string number)
        {
            for (int i = 0; i < studentCount; i++)
            {
                if (string.Equals(students[i].StudentNumber, number, StringComparison.OrdinalIgnoreCase))
                    return i;
            }
            return -1;
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

        static void PrintStudent(Student s)
        {
            Console.WriteLine("Student Number: " + s.StudentNumber);
            Console.WriteLine("Name: " + s.Name);
            Console.WriteLine("Program: " + s.Program);
            Console.WriteLine("Year Level: " + s.YearLevel);
        }
    }
}
