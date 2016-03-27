using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XinemaActual.Models
{
    public class Movie
    {
        [Key]
        
        public int movieID { get; set; }

        public string movieTitle { get; set; }
        public string movieGenre { get; set; }
        public string movieRating { get; set; }
        public string movieTrailerURL { get; set; }
        public string movieRunningTime { get; set; }
        public string movieLanguage { get; set; }
        public string moveShowTimes { get; set; }

        public override string ToString()
        {
            return "Title : " + movieTitle + "Running time : " + movieRunningTime + "Rated : " + movieRating + "Genre : " + movieGenre + "Language : " + movieLanguage + "ShowTime : " + moveShowTimes;
        }
    }
}