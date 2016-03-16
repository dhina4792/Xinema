using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace XinemaActual.ViewModels
{
    public class ShowtimeVM
    {
        public int showtimeID { get; set; }
        [DataType(DataType.Date)]
        public string showtimeDate { get; set; }

        public string showtimeStartTime { get; set; }

        public int movieID { get; set; }
        public string movieTitle { get; set; }
        public string movieRating { get; set; }

        public int cinemaID { get; set; }
        public string cinemaName { get; set; }
    }
}