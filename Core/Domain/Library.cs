using Core.Shared.ModelViews;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Library
    {
     
        public int Id { get; set; }


        public string Name { get; set; }

      
        public string Location { get; set; }

        public ICollection<Book> Books { get; set; }


    }
}
