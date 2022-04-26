using Core.Domain;
using Data.Context;
using Manager.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _dataContext;

        public BookRepository(DataContext context)
        {
            _dataContext = context;
        }
        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {

            return await _dataContext.Books.ToListAsync();
        }

        public async Task<Book> GetBookAsync(int id)
        {
            return await _dataContext.Books.FindAsync(id);
        }

        public async Task<Book> CreateBookAsync(Book book)
        {
            await _dataContext.Books.AddAsync(book);
            await _dataContext.SaveChangesAsync();
            return book;
        }
        public async Task<Book> UpdateBookAsync(Book book)
        {
            var bookUpdated = await _dataContext.Books.FindAsync(book.Id);

            if (bookUpdated == null)
            {
                return null;
            }

            _dataContext.Entry(bookUpdated).CurrentValues.SetValues(book);

            _dataContext.Books.Update(bookUpdated);
            await _dataContext.SaveChangesAsync();
            return bookUpdated;
        }

        public async Task DeleteBookAsync(int id)
        {
            var bookToDelete = await _dataContext.Books.FindAsync(id);
            _dataContext.Books.Remove(bookToDelete);
            await _dataContext.SaveChangesAsync();
        }
        public async Task<bool> ExistAsync(int id)
        {
            return await _dataContext.Books.AnyAsync(p => p.Id == id);
        }
    }
}
