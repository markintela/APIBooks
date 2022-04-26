using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class ErrorResponse
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string  Message { get; set; }

        public ErrorResponse() { 
            Id = Guid.NewGuid();
            Date = DateTime.Now;
            Message = "Unexpected error.";
        }
    }
}
