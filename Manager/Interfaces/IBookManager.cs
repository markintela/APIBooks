using Core.Domain;
using Core.Shared.ModelViews;
using Core.Shared.ModelViews.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Interfaces
{
    public interface IBookManager
    {
        Task<IEnumerable<BookView>> GetAllBooksAsync();

        Task<BookView> GetBookAsync(int id);

        Task<BookView> CreateBookAsync(NewBook book);

        Task<BookView> UpdateBookAsync(AlterBook book);

        Task DeleteBookAsync(int id);

    }
}
