using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class FlightInfoPartial
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string SourceId { get; set; }
        public double Long { get; set; }
        public double Lat { get; set; }
        public DateTime DateAndTime { get; set; }
        public string SourceFilter { get; set; }
        public string Destination { get; set; }
        public string FlightCode { get; set; }
    }
}
