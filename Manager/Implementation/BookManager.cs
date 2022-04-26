using AutoMapper;
using Core.Domain;
using Core.Shared.ModelViews;
using Core.Shared.ModelViews.Book;
using Manager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Implementation
{
    public class BookManager : IBookManager
    {
        private readonly IBookRepository _bookRepository;

        public readonly IMapper _mapper;

        public BookManager(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<BookView>> GetAllBooksAsync()
        {
            return _mapper.Map<IEnumerable<Book>, IEnumerable<BookView>>(await _bookRepository.GetAllBooksAsync());
        }

        public async Task<BookView> GetBookAsync(int id)
        {
            return _mapper.Map<BookView>(await _bookRepository.GetBookAsync(id));
        }

        public async Task<BookView> CreateBookAsync(NewBook newBook)
        {
            var Book = _mapper.Map<Book>(newBook);
            return _mapper.Map<BookView>(await _bookRepository.CreateBookAsync(Book));
        }

        public async Task<BookView> UpdateBookAsync(AlterBook alterBook)
        {
            var book = _mapper.Map<Book>(alterBook);
            return _mapper.Map<BookView>(await _bookRepository.UpdateBookAsync(book));
        }

        public async Task DeleteBookAsync(int id)
        {
            await _bookRepository.DeleteBookAsync(id);
        }

        
    }
}
