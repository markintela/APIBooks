using Core.Domain;
using Core.Shared.ModelViews;
using Core.Shared.ModelViews.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Interfaces
{
    public interface ILibraryManager
    {
        Task<IEnumerable<LibraryView>> GetAllLibrariesAsync();

        Task<LibraryView> GetLibraryAsync(int id);

        Task<LibraryView> CreateLibraryAsync(NewLibrary library);

        Task<LibraryView> UpdateLibraryAsync(AlterLibrary alterLibrary);

        Task DeleteLibraryAsync(int id);
    }
}
