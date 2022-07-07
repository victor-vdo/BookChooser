using BookChooser.DTO;
using BookChooser.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace BookChooser.Services
{
    public class BookService
    {
        public List<Book> Books { get; set; }

        public List<Book> GetBooks(string path)
        {
            string text = "";
            using (var file = new StreamReader(path))
            {
                text = file.ReadToEnd();
                var data = JsonConvert.DeserializeObject<Data>(text);
                Books = data.Books;
            }
            return Books;
        }

        public void AddBook(string path, Book book)
        {
            Books = GetBooks(path);
            using (var file = new StreamWriter(path))
            {
                Books.Add(book);
                dynamic data = new { Books };
                var books = JsonConvert.SerializeObject(data);
                file.Write(books);
            }
        }
        public void RemoveBook(string path, Book book)
        {
            string text = "";
            using (var file = new StreamReader(path))
            {
                text = file.ReadToEnd();
                var booksArray = JsonConvert.DeserializeObject<List<Book>>(text);
                foreach (var item in booksArray)
                {
                    if(book == item)
                        Books.Remove(book);
                }
            }
        }
    }
}
