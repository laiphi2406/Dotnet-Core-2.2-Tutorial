using System.Collections.Generic;
using System.Linq;
using BooksApi.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
namespace BooksApi.Services{
    public class BookService{
        private readonly IMongoCollection<Book> _books;
        public BookService(IConfiguration config){
            var client = new MongoClient(config.GetConnectionString("BookstoreDb"));
            var datatbase = client.GetDatabase("BookstoreDb");
            _books = datatbase.GetCollection<Book>("Books");
        }
        public List<Book> GetBooks(){
            return _books.Find(book => true).ToList();
        }
        public Book GetBook(string id){
            return _books.Find<Book>(Book=>Book.Id==id).FirstOrDefault();
        }
        public Book Create(Book book){
            _books.InsertOne(book);
            return book;
        }
        public void Update(string id, Book bookIn){
            _books.ReplaceOne(book => book.Id==bookIn.Id,bookIn);
        }
        public void Remove(string id){
            _books.DeleteOne(book => book.Id==id);
        }
        public void Remove(Book bookIn){
            _books.DeleteOne(book => book.Id==bookIn.Id);
        }
    }
}
