using System;

namespace UniversalWebApi.Helpers.ExceptionManager
{
    public class ExceptionContract
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Method { get; set; }
        
        public string Class { get; set; }
        
        public string Arguments { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
    }
}