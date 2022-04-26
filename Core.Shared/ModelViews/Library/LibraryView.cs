using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Shared.ModelViews
{
    public class LibraryView
    {

        public int Id { get; set; }


        public string Name { get; set; }

        public string Location { get; set; }

        public ICollection<BookView> Books { get; set; }
    }
}
