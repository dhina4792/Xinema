using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace XinemaActual.Models
{
    public class Showtime
    {
        public int showtimeID { get; set; }
        [DataType(DataType.Time)]
        public string showtimeStartTime { get; set; }
        [DataType(DataType.Date)]
        public string showtimeDate { get; set; }
    }
}