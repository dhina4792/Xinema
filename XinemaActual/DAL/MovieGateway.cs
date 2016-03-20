using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XinemaActual.Models;

namespace XinemaActual.DAL
{
    public class MovieGateway : DataGateway<Movie>
    {
        private List<SelectListItem> MovieOrderByDropDownItems;

        public MovieGateway()
        {

            MovieOrderByDropDownItems = new List<SelectListItem>();
            var firstItem = new SelectListItem { Text = "A-Z", Value = "0" };
            var secondItem = new SelectListItem { Text = "Z-A", Value = "1" };


            MovieOrderByDropDownItems.Add(firstItem);
            MovieOrderByDropDownItems.Add(secondItem);


        }

        public List<SelectListItem> GetMovieOrderByNames()
        {
            return MovieOrderByDropDownItems;
        }
        public IEnumerable<Movie> SortBy(int id)
        {
            switch (id)
            {

                case 1:

                    return SelectAllMovies().OrderByDescending(t => t.movieTitle);



            }
            return SelectAllMovies().OrderBy(t => t.movieTitle);
        }

        public IEnumerable<Movie> SelectAllMovies()
        {
            IEnumerable<Movie> groupedMovies= data.GroupBy(t => t.movieTitle).Select(t => t.FirstOrDefault());
            return groupedMovies;
                                                      
        }
    }
}