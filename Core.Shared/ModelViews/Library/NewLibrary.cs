using Core.Shared.ModelViews.Book;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Shared.ModelViews
{
    public class NewLibrary
    {

        public string Name { get; set; }


        public string Location { get; set; }

        public ICollection<ReferenceBook> Books { get; set; }
    }
}
