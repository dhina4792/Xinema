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
        private List<SelectListItem> MovieGenreByDropDownItems;

        public MovieGateway()
        {

            MovieOrderByDropDownItems = new List<SelectListItem>();
            MovieGenreByDropDownItems = new List<SelectListItem>();
            var firstItem = new SelectListItem { Text = "A-Z", Value = "0" };
            var secondItem = new SelectListItem { Text = "Z-A", Value = "1" };


            MovieOrderByDropDownItems.Add(firstItem);
            MovieOrderByDropDownItems.Add(secondItem);

        }
        public List<SelectListItem> GetMovieGenres()
        {
            MovieGenreByDropDownItems = new List<SelectListItem>();
            var moviesGenres = data.GroupBy(t => t.movieGenre).ToArray();
            int i = 0;
            MovieGenreByDropDownItems.Add(new SelectListItem { Text = "All", Value = null, Selected = true });

            foreach (var item in moviesGenres)
            {

                MovieGenreByDropDownItems.Add(new SelectListItem { Text = item.Key.ToString(), Value = i.ToString(), Selected = false });
                i++;
            }

            return MovieGenreByDropDownItems;
        }
        public List<SelectListItem> GetMovieOrderByNames()
        {
            return MovieOrderByDropDownItems;
        }

        internal object SelectMovieByMovieByGenres(int gID)
        {
            string genreName = MovieGenreByDropDownItems.Where(item => item.Value == gID.ToString()).First().Text;
            return data.Where(t => t.movieGenre == genreName);
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
            IEnumerable<Movie> groupedMovies = data.GroupBy(t => t.movieTitle).Select(t => t.FirstOrDefault());
            return groupedMovies;
        }
        public IEnumerable<Movie> SelectAllGenres()
        {
            IEnumerable<Movie> groupedGenres = data.GroupBy(t => t.movieGenre).Select(t => t.FirstOrDefault());
            return groupedGenres;
        }

        public IEnumerable<Movie> getRatings(string genre)
        { 
            IEnumerable<Movie> movieRatings = data.Where(t=>t.movieGenre == " " + genre + " ");
            return movieRatings;
        }
    }
}