using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace XinemaActual.Models
{
    public class Movie
    {
        public int movieID { get; set; }
        public string movieName { get; set; }
    }
}