using AutoMapper;
using Core.Domain;
using Core.Shared.ModelViews;
using Core.Shared.ModelViews.Library;
using Manager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Implementation
{
    public class LibraryManager: ILibraryManager
    {
        private readonly ILibraryRepository _libraryRepository;

        public readonly IMapper _mapper;

        public LibraryManager(ILibraryRepository libraryRepository, IMapper mapper)
        {
            _libraryRepository = libraryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LibraryView>> GetAllLibrariesAsync()
        {
            return  _mapper.Map<IEnumerable<Library>, IEnumerable<LibraryView>>(await _libraryRepository.GetAllLibrariesAsync());
        }

        public async Task<LibraryView> GetLibraryAsync(int id)
        {
            return _mapper.Map<LibraryView> (await _libraryRepository.GetLibraryAsync(id));
        }

        public async Task<LibraryView> CreateLibraryAsync(NewLibrary newLibrary)
        {
            var library = _mapper.Map<Library>(newLibrary);
            return _mapper.Map<LibraryView>(await _libraryRepository.CreateLibraryAsync(library));
        }

        public async Task<LibraryView> UpdateLibraryAsync(AlterLibrary alterLibrary)
        {
            var library = _mapper.Map<Library>(alterLibrary);
            return  _mapper.Map<LibraryView>(await _libraryRepository.UpdateLibraryAsync(library));
        }

        public async Task DeleteLibraryAsync(int id)
        {
            await _libraryRepository.DeleteLibraryAsync(id);
        }

    }
}
