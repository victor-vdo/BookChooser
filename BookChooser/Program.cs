using BookChooser.Models;
using BookChooser.Services;
using System;
using System.Collections.Generic;

namespace BookChooser
{
    internal static class Program
    {
        const string Path = @"Infra\Data\books.json";
        private static Random rng = new Random();
        static void Main(string[] args)
        {
            Menu();
        }

        static void Menu()
        {
            Console.Clear(); 
            Console.WriteLine("Select an option:");
            Console.WriteLine("1 - Find registered books");
            Console.WriteLine("2 - Register a new book ");
            Console.WriteLine("3 - Choose a random book ");
            Console.WriteLine("ESC - Leave ");
            Console.WriteLine("----------------------------");

            do
            {
                var option = Int32.TryParse(Console.ReadLine(), out int result) ? result : 0;
                switch (option)
                {
                    case 1:
                        BookFinder();
                        break;
                    case 2:
                        BookRegister();
                        break;
                    case 3:
                        BookChooser();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid option! Try again.");
                        Console.WriteLine();
                        Menu();
                        break;
                }
            }
            while (Console.ReadKey().Key != ConsoleKey.Escape);
        }

        static void LeaveMenu()
        {
            Console.WriteLine("Would you like to get back to the main menu?");
            Console.WriteLine("1 - Yes");
            Console.WriteLine("2 - No");
            var option = short.TryParse(Console.ReadLine(), out short result) ? result : 0;
            switch (option)
            {
                case 1:
                    Menu();
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Thank you for your time.");
                    Console.WriteLine("Application will be finished!");
                    Environment.Exit(0);
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid option! ");
                    Console.WriteLine("Application will be finished!");
                    Environment.Exit(0);
                    break;
            }
        }

        static void BookFinder()
        {
            string path = Path;
            Console.Clear();
            BookService service = new BookService();
            var books = service.GetBooks(path);
            foreach (var book in books)
            {
                Console.WriteLine($"Title: {book.Title}");
                Console.WriteLine($"Description: {book.Description}");
                Console.WriteLine("---");
            }
            LeaveMenu();
        }

        static void BookRegister()
        {
            try
            {
                string path = Path;
                Console.Clear();
                BookService service = new BookService();
                Book book = new Book();

                Console.WriteLine("Book register:");
                Console.WriteLine($"Type the title below:");
                book.Title = Console.ReadLine();
                Console.WriteLine($"Type the description below:");
                book.Description = Console.ReadLine();

                service.AddBook(path, book);
                Console.WriteLine($"Title: {book.Title}");
                Console.WriteLine($"Description: {book.Description}");
                Console.WriteLine($"Book registred successfuly!");
                LeaveMenu();
            }
            catch (Exception)
            {
                Console.WriteLine("Error trying to register the book. Try again!");
                Menu();
            }
        }

        static void BookChooser()
        {
            string path = Path;
            Console.Clear();
            BookService service = new BookService();
            var books = service.GetBooks(path);
            var book = Shuffle(books);
            Console.WriteLine($"Choosing a random book...");
            Console.WriteLine($"Title: {book?.Title}");
            Console.WriteLine($"Description: {book?.Description}");
            Console.WriteLine("---");
            LeaveMenu();
        }

        public static T Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            T value = default(T);
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return value;
        }

    }
}
