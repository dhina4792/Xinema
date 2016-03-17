using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace XinemaActual.Models
{
    public class Promotion
    {
        [Key]
        public int promotionID { get; set; }

        public string promotionDescription { get; set; }
        public string promotionCinemaName { get; set; }
    }
}