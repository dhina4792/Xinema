using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace XinemaActual.Models
{
    public class Cinema
    {
        public int CinemaID { get; set; }
        public string CinemaName { get; set; }
        public string CinemaAddress { get; set; }
    }
}