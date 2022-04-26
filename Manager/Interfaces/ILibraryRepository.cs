using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Interfaces
{
    public interface ILibraryRepository
    {

        Task<IEnumerable<Library>> GetAllLibrariesAsync();

        Task<Library> GetLibraryAsync(int id);

        Task<Library> CreateLibraryAsync(Library library);

        Task<Library> UpdateLibraryAsync(Library library);

        Task DeleteLibraryAsync(int id);
    }
}
