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

        [DataType(DataType.Time)]
        public string showtimeStartTime { get; set; }

        [DataType(DataType.Date)]
        public string showtimeDate { get; set; }

        public int? cinemas_cinemasID { get; set; }
        [ForeignKey("cinemas_cinemasID")]
        public virtual Cinema Cinema { get; set; }

        public int? movies_moviesID { get; set; }
        [ForeignKey("movies_moviesID")]
        public virtual Movie Movie { get; set; }

        public string movieTitle { get; set; }
        public string cinemaName { get; set; }

    }
}