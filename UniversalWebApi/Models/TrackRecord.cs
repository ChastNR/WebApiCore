using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversalWebApi.Models
{
    public class TrackRecord
    {
        public int Id { get; set; }
        public string TrackName { get; set; }
        public int Laps { get; set; }
        public float Time { get; set; }
        public string Name { get; set; }
    }
}
