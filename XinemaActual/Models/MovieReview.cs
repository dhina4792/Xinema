using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace XinemaActual.Models
{
    
    public class MovieReview
    {
        [Key]
        public int movieReviewID { get; set; }
        
        public string movieReviewName { get; set; }

        public string movieReviewIMDB { get; set; }

        public string movieReviewRottenTomato { get; set; }
       
    }
}