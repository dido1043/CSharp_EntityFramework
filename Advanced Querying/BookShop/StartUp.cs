namespace BookShop
{
    using BookShop.Models;
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using Microsoft.EntityFrameworkCore.Update.Internal;
    using System.Data.Entity.SqlServer;
    using System.Text;

    //using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

    public class StartUp
    {
        public static void Main()
        {
            using var db = new BookShopContext();
            //DbInitializer.ResetDatabase(db);
            var command = int.Parse(Console.ReadLine());
            var result = GetBooksNotReleasedIn(db, command);
            Console.WriteLine(result);
        }

        //2.Age Restriction
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {

            
            AgeRestriction ageRestriction = Enum.Parse<AgeRestriction>(command, true);
            string[] bookTitles = context.Books
                .Where(b => b.AgeRestriction == ageRestriction)
                .Select(b => b.Title)
                .OrderBy(t => t)
                .ToArray(); 

            return String.Join(Environment.NewLine, bookTitles);
        }
        //3.Golden Books
        public static string GetGoldenBooks(BookShopContext context)
        {
            var editionType = "Gold";
            var bookType = Enum.Parse<EditionType>(editionType, true);
            var bookTitles = context.Books
                .Select(b => new { 
                    b.BookId,
                    b.Title,
                    b.EditionType,
                    b.Copies
                })
                .Where(b => b.Copies < 5000 && b.EditionType == bookType)
                .OrderBy(b => b.BookId)
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var book in bookTitles)
            {
                sb.AppendLine(book.Title);
            }
        
            return sb.ToString().TrimEnd();
        }
        //4. Books by Price
        public static string GetBooksByPrice(BookShopContext context)
        {

            var books = context.Books
                .Select(b => new
                {
                    b.BookId,
                    b.Title,
                    b.Price
                })
                .Where(bp => bp.Price > 40)
                .OrderByDescending(b => b.Price)
                .ToList();
            StringBuilder sb = new StringBuilder();
            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - ${book.Price}");
            }
            return sb.ToString().TrimEnd();
        }
        //5.Not Released In
        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {

            StringBuilder sb = new StringBuilder();
            var books = context.Books
                .Select(bk => new
                {
                    bk.BookId,
                    bk.Title,
                    bk.ReleaseDate
                })
                .Where(rd =>  rd.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId);

            foreach (var book in books)
            {
                sb.AppendLine(book.Title);
            }
            return sb.ToString().TrimEnd();
        }
    }
}


