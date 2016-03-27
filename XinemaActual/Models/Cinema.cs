using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace XinemaActual.Models
{
    public class Cinema
    {
        [Key]
        public int cinemaID { get; set; }

        public string cinemaName { get; set; }
        public string cinemaAddress { get; set; }

        public List<Movie> cinemaMovies { get; set; }

        public string displayMovies()
        {

            foreach (Movie m in cinemaMovies)
            {
                return m.ToString();
            }

            return null;


        }

        public override string ToString()
        {
            return cinemaName;
        }
    }
}