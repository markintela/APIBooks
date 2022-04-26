using Core.Domain;
using Core.Shared.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Interfaces
{
    public interface IBookRepository
    {
     

        Task<bool> ExistAsync(int id);

        Task<IEnumerable<Book>> GetAllBooksAsync();

        Task<Book> GetBookAsync(int id);

        Task<Book> CreateBookAsync(Book book);

        Task<Book> UpdateBookAsync(Book book);

        Task DeleteBookAsync(int id);


    }
}
