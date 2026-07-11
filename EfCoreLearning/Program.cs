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

        
        // var allBooks = await db.Books.ToListAsync();
        // // foreach(var el in allBooks)
        // // {
        // // System.Console.WriteLine($"Автор: {el.Author} - книга: {el.Title} {el.Year} года выпуска");
        // // }

        // // var book1 = await db.Books.FindAsync(2);
        // //     db.Books.Remove(book1);
        // //     await db.SaveChangesAsync();

        // foreach (var el in allBooks)
        // {
        //     db.Books.Remove(el);
        // }
        // await db.SaveChangesAsync();
        

        // var author1 = new Author{Name="Столяров"};
        // db.Authors.Add(author1);
        // await db.SaveChangesAsync();
        
        // var book1 = new Book{Title="Азы Программирования", Year=2019, AuthorId=author1.Id};
        // db.Books.Add(book1);
        // await db.SaveChangesAsync();

        // var books = await db.Books.Include(b => b.Author).ToListAsync();
        // foreach (var el in books)
        // Console.WriteLine($"{el.Title} — {el.Author.Name}");

        // var author = new Author{Name="Джек Лондон"};
        // db.Authors.Add(author);
        // await db.SaveChangesAsync();

        // var book2 = new Book{Title="Азы Программирования 2", Year=2020, AuthorId=author1.Id};
        // db.Books.Add(book2);
        // await db.SaveChangesAsync();

        // var book3 = new Book{Title="Мартин Иден", Year=1999, AuthorId=author.Id};
        // db.Books.Add(book3);
        // await db.SaveChangesAsync();

        // var books= await db.Books.Include(b=>b.Author).ToListAsync();
        // foreach(var el in books)
        // {
        //     Console.WriteLine($"{el.Title} — {el.Author.Name}");
        // }


        // var allBooks = await db.Books.ToListAsync();
        // var allAuthors = await db.Authors.ToListAsync();
        // foreach(var el in allBooks)
        // {
        //     db.Books.Remove(el);
        // }
        // foreach(var el in allAuthors)
        // {
        //     db.Authors.Remove(el);
        // }
        // await db.SaveChangesAsync();

        var author1 = new Author{Name="Джордж Оруэлл"};
        db.Authors.Add(author1);
        var author2 = new Author{Name="Рэй Брэдбери"};
        db.Authors.Add(author2);
        await db.SaveChangesAsync();
        
        var book1 = new Book{Title="1984", AuthorId=author1.Id, Year=1949};
        db.Books.Add(book1);
        var book2 = new Book{Title="Скотный двор", AuthorId=author1.Id,Year= 1945};
        db.Books.Add(book2);
        var book3 = new Book{Title="451 градус по Фаренгейту",AuthorId=author2.Id, Year=1953};
        db.Books.Add(book3);
        await db.SaveChangesAsync();

        var bookUp= await db.Books.FindAsync(book1.Id);
        bookUp.Year = 1948;
        await db.SaveChangesAsync();

        var bookDel = await db.Books.FindAsync(book2.Id);
        db.Books.Remove(bookDel);
        await db.SaveChangesAsync();

    }
}