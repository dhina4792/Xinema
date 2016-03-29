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
        public string moviePlot { get; set; }
        public string movieActors { get; set; }
        public string moviePoster { get; set; }
        public string movieDirector { get; set; }
        public string movieWebsiteURL { get; set; }
        public string movieIMDBRating { get; set; }
        public string movieTomatoesRating { get; set; }


    }
}