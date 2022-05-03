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


    public class LibraryRepository : ILibraryRepository
    {
        private readonly DataContext _dataContext;

        public LibraryRepository(DataContext context)
        {
            this._dataContext = context;
        }


        public async Task<IEnumerable<Library>> GetAllLibrariesAsync(){

            return await _dataContext.Libraries.Include(b => b.Books).ToListAsync();
        }
        public async Task<Library> GetLibraryAsync(int id)
        {

            return await _dataContext.Libraries.Include(b => b.Books).SingleOrDefaultAsync(p => p.Id == id);
        }


        public async Task<Library> CreateLibraryAsync(Library library)
        {         
            await _dataContext.Libraries.AddAsync(library);
            await InsertLibraryBooks(library);
            await _dataContext.SaveChangesAsync();
            return library;
        }


        private async Task InsertLibraryBooks(Library library)
        {
            var booksearch = new List<Book>();
            foreach (var book in library.Books)
            {
                var bookFound = await _dataContext.Books.FindAsync(book.Id);
                booksearch.Add(bookFound);
            }
            library.Books = booksearch;
        }


        public async Task<Library> UpdateLibraryAsync(Library library)
        {
            var librarysearch = await _dataContext.Libraries
                                    .Include(b => b.Books)
                                    .SingleOrDefaultAsync(p => p.Id == library.Id);
            if (librarysearch == null)
            {
                return null;
            }
            _dataContext.Entry(librarysearch).CurrentValues.SetValues(library);
            await UpdateLibraryBooks(library, librarysearch);
            await _dataContext.SaveChangesAsync();
            return librarysearch;
        }

        private async Task UpdateLibraryBooks(Library library, Library librarySearch)
        {
            librarySearch.Books.Clear();
            foreach (var book in library.Books)
            {
                var bookFound = await _dataContext.Books.FindAsync(book.Id);
                librarySearch.Books.Add(bookFound);
            }
        }
        public async Task DeleteLibraryAsync(int id)
        {
            var libraryToDelete =  await _dataContext.Libraries.FindAsync(id);
            _dataContext.Libraries.Remove(libraryToDelete);
            await _dataContext.SaveChangesAsync();
        }

    }
}
