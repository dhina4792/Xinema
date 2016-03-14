using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using XinemaActual.Models;

namespace XinemaActual.DAL
{
    public class XinemaActualContext : DbContext
    {
        public XinemaActualContext()
            : base("XinemaActualDB")
        {
            
        }

        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Movie> Movies { get; set; }
    }
}