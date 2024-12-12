using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace bookStoreApp
{ 
    public class databaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public databaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<User>().Wait();
            _database.CreateTableAsync<Book>().Wait();
        }

        public Task<int> SaveUserAsync(User user) => _database.InsertAsync(user);
        public Task<User> GetUserAsync(string email, string password) =>
            _database.Table<User>().FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        // Add a new user
        public Task<int> AddUserAsync(User user)
        {
            return _database.InsertAsync(user);
        }

        // Get user by email and password
        public Task<User> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            return _database.Table<User>()
                            .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }

        // Book table
        public Task<int> SaveBookAsync(Book book) => _database.InsertAsync(book);
        public Task<List<Book>> GetBooksAsync() => _database.Table<Book>().ToListAsync();
        public Task<List<Book>> GetBooksByGenreAsync(string genre) =>
            _database.Table<Book>().Where(b => b.Genre == genre).ToListAsync();
        public Task<int> DeleteBookAsync(Book book) => _database.DeleteAsync(book);
        public Task<int> UpdateBookAsync(Book book) => _database.UpdateAsync(book);
        public Task<int> AddBookAsync(Book book)
        {
            return _database.InsertAsync(book);
        }
    }
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class Book
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Genre { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public decimal? Price { get; set; }
        public int Quantity { get; set; }
    }
}
