using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class ErrorResponse
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string  Message { get; set; }

        public ErrorResponse(string id, string message) { 
            Id = id;
            Date = DateTime.Now;
            Message = message;
        }
    }
}
