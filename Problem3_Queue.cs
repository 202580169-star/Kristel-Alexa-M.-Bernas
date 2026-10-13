using System;
using System.Collections.Generic;

namespace Problem3
{
    
    struct StudentRequest
    {
        public string StudentNumber;
        public string StudentName;
        public string RequestType;
    }

    class Program
    {
        static Queue<StudentRequest> requestQueue = new Queue<StudentRequest>();

        static void Main()
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n========================================");
                Console.WriteLine("           STUDENT REQUEST QUEUE");
                Console.WriteLine("========================================\n");
                Console.WriteLine("1. Add Request");
                Console.WriteLine("2. View Pending Requests");
                Console.WriteLine("3. Process Request");
                Console.WriteLine("4. Exit\n");
                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddRequest(); break;
                    case "2": ViewPending(); break;
                    case "3": ProcessRequest(); break;
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
        static void AddRequest()
        {
            StudentRequest r = new StudentRequest();

            Console.Write("\nEnter Student Number: ");
            r.StudentNumber = Console.ReadLine();
            Console.Write("Enter Student Name: ");
            r.StudentName = Console.ReadLine();
            Console.Write("Enter Request Type (e.g. Certificate of Enrollment, Student ID, Transcript Request): ");
            r.RequestType = Console.ReadLine();

            requestQueue.Enqueue(r);

            Console.WriteLine("\nRequest added successfully!");
        }
        static void ViewPending()
        {
            if (requestQueue.Count == 0)
            {
                Console.WriteLine("\nThere are no pending requests.");
                return;
            }

            Console.WriteLine("\nREQUEST QUEUE");

            int position = 1;
            foreach (StudentRequest r in requestQueue)
            {
                Console.WriteLine(position + ". " + r.StudentName + " - " + r.RequestType);
                position++;
            }
        }

        static void ProcessRequest()
        {
            if (requestQueue.Count == 0)
            {
                Console.WriteLine("\nThere are no pending requests to process.");
                return;
            }

            StudentRequest r = requestQueue.Dequeue();

            Console.WriteLine("\nProcessing Request: " + r.StudentName + " - " + r.RequestType);
            Console.WriteLine("\nRequest processed successfully!");
        }
    }
}
