using System.Text.RegularExpressions;
using LibraryManagementApp.BusinessLogic.Services;
using LibraryManagementApp.DataAccess.Repositories;
using LibraryManagementApp.Models;
using Microsoft.Extensions.Configuration;

namespace LibraryManagementApp
{
    class Program
    {
        private static BookService _bookService;
        private static MemberService _memberService;
        private static LoanService _loanService;

        static void Main(string[] args)
        {
            InitializeServices();
            ShowMainMenu();
        }

        private static void InitializeServices()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = config.GetConnectionString("LibraryManagement");

            var bookRepo = new BookRepository(connectionString);
            var memberRepo = new MemberRepository(connectionString);
            var loanRepo = new LoanRepository(connectionString);

            _bookService = new BookService(bookRepo);
            _memberService = new MemberService(memberRepo);
            _loanService = new LoanService(loanRepo, bookRepo, memberRepo);
        }

        private static void ShowMainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Library Management System ===");
                Console.WriteLine("1. Add New Book");
                Console.WriteLine("2. Register Member");
                Console.WriteLine("3. Borrow Book");
                Console.WriteLine("4. Return Book");
                Console.WriteLine("5. View Borrowed Books");
                Console.WriteLine("6. Search Books");
                Console.WriteLine("7. Exit");
                Console.Write("\nChoose an option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        AddNewBook();
                        break;
                    case "2":
                        RegisterMember();
                        break;
                    case "3":
                        BorrowBook();
                        break;
                    case "4":
                        ReturnBook();
                        break;
                    case "5":
                        ViewBorrowedBooks();
                        break;
                    case "6":
                        SearchBooks();
                        break;
                    case "7":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid option!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void AddNewBook()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== Add New Book ===");

                var book = new Book();
                book.Title = GetNonEmptyInput("Title");
                book.Author = GetNonEmptyInput("Author");
                book.ISBN = GetNonEmptyInput("ISBN");
                book.PublicationYear = GetValidYearInput();

                _bookService.AddBook(book);
                ShowSuccessMessage("Book added successfully!");
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private static void RegisterMember()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== Register Member ===");

                var member = new Member();
                member.FirstName = GetNonEmptyInput("First Name");
                member.LastName = GetNonEmptyInput("Last Name");
                member.Email = GetValidEmailInput();
                member.PhoneNumber = GetValidPhoneNumberInput();

                _memberService.RegisterMember(member);
                ShowSuccessMessage("Member registered successfully!");
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private static void BorrowBook()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== Borrow Book ===");

                var bookId = GetValidIntInput("Enter Book ID: ");
                var memberId = GetValidIntInput("Enter Member ID: ");

                _loanService.BorrowBook(bookId, memberId);
                ShowSuccessMessage("Book borrowed successfully!");
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private static void ReturnBook()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== Return Book ===");

                var bookId = GetValidIntInput("Enter Book ID to return: ");
                _loanService.ReturnBook(bookId);
                ShowSuccessMessage("Book returned successfully!");
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private static void ViewBorrowedBooks()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== Currently Borrowed Books ===\n");

                var loans = _loanService.GetActiveLoans();
                if (!loans.Any())
                {
                    Console.WriteLine("No books currently borrowed");
                }
                else
                {
                    foreach (var loan in loans)
                    {
                        Console.WriteLine($"Loan ID: {loan.LoanId}");
                        Console.WriteLine($"Book: {loan.Title}");
                        Console.WriteLine($"Borrower: {loan.FirstName} {loan.LastName}");
                        Console.WriteLine($"Borrow Date: {loan.BorrowDate:yyyy-MM-dd}");
                        Console.WriteLine($"Due Date: {loan.DueDate:yyyy-MM-dd}");
                        Console.WriteLine(new string('-', 40));
                    }
                }
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private static void SearchBooks()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== Search Books ===");
                Console.Write("Enter search term (title/author): ");
                var searchTerm = Console.ReadLine();

                var results = _bookService.SearchBooks(searchTerm);
                Console.WriteLine("\nSearch Results:");

                if (!results.Any())
                {
                    Console.WriteLine("No books found");
                }
                else
                {
                    foreach (var book in results)
                    {
                        Console.WriteLine($"ID: {book.BookId}");
                        Console.WriteLine($"Title: {book.Title}");
                        Console.WriteLine($"Author: {book.Author}");
                        Console.WriteLine($"ISBN: {book.ISBN}");
                        Console.WriteLine($"Year: {book.PublicationYear}");
                        Console.WriteLine(new string('-', 40));
                    }
                }
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        #region Helper Methods
        private static int GetValidIntInput(string prompt)
        {
            int result;
            do
            {
                Console.Write(prompt);
            } while (!int.TryParse(Console.ReadLine(), out result));
            return result;
        }

        private static string GetNonEmptyInput(string fieldName)
        {
            string input;
            do
            {
                Console.Write($"{fieldName}: ");
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine($"{fieldName} cannot be empty!");
                }
            } while (string.IsNullOrWhiteSpace(input));
            return input;
        }

        private static int GetValidYearInput()
        {
            int year;
            do
            {
                Console.Write("Publication Year: ");
            } while (!int.TryParse(Console.ReadLine(), out year) || year > DateTime.Now.Year);
            return year;
        }

        private static string GetValidEmailInput()
        {
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            string email;
            do
            {
                Console.Write("Email: ");
                email = Console.ReadLine();
                if (!emailRegex.IsMatch(email))
                {
                    Console.WriteLine("Invalid email format!");
                }
            } while (!emailRegex.IsMatch(email));
            return email;
        }

        private static string GetValidPhoneNumberInput()
        {
            var phoneRegex = new Regex(@"^\+?[1-9]\d{1,14}$"); // E.164 format
            string phoneNumber;
            do
            {
                Console.Write("Phone Number: ");
                phoneNumber = Console.ReadLine();
                if (!phoneRegex.IsMatch(phoneNumber))
                {
                    Console.WriteLine("Invalid phone number format!");
                }
            } while (!phoneRegex.IsMatch(phoneNumber));
            return phoneNumber;
        }

        private static void ShowSuccessMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.ReadKey();
        }

        private static void ShowErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {message}");
            Console.ResetColor();
            Console.ReadKey();
        }
        #endregion
    }
}