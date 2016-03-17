using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XinemaActual.Models
{
    public class Showtime
    {
        [Key]
        public int showtimeID { get; set; }


        public string showtimeStartTime { get; set; }


        public string showtimeDate { get; set; }

        public string showtimeTitle { get; set; }
        public string showtimeCinemaName { get; set; }

    }
}