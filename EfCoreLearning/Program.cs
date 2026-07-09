using System;
using Microsoft.EntityFrameworkCore;
namespace EfCoreLearning;
public class Program
{
    static async Task Main()
    {
        using var db = new AppDbContext();
        // var book = new Book{Title="Азы программирования", Author="Андрей Столяров", Year=2019};
        // db.Books.Add(book);
        // await db.SaveChangesAsync();

        // var allBooks = await db.Books.ToListAsync();
        // foreach(var el in allBooks)
        // {
        // System.Console.WriteLine($"Автор: {el.Author} - книга: {el.Title} {el.Year} года выпуска");
        // }

        // var book = await db.Books.FindAsync(1);
        // book.Year = 2020;
        // await db.SaveChangesAsync();

        
        var allBooks = await db.Books.ToListAsync();
        // foreach(var el in allBooks)
        // {
        // System.Console.WriteLine($"Автор: {el.Author} - книга: {el.Title} {el.Year} года выпуска");
        // }

        // var book1 = await db.Books.FindAsync(2);
        //     db.Books.Remove(book1);
        //     await db.SaveChangesAsync();

        foreach (var el in allBooks)
        {
            db.Books.Remove(el);
        }
        await db.SaveChangesAsync();
        

    }
}