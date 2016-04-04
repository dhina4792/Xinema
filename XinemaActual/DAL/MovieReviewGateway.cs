using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using XinemaActual.DAL;
using XinemaActual.Models;

namespace XinemaActual.DAL
{
    public class MovieReviewGateway : DataGateway<MovieReview>
    {
        private List<SelectListItem> MovieGenreByDropDownItems;
        private MovieGateway movieReviewGateway;

        public MovieReviewGateway()
        {
            MovieGenreByDropDownItems = new List<SelectListItem>();
        }
        public IEnumerable<MovieReview> SelectAllMoviesReviews()
        {
            IEnumerable<MovieReview> groupedMoviesReviews = data.GroupBy(t => t.movieReviewName).Select(t => t.FirstOrDefault());
            return groupedMoviesReviews;

        }

        internal dynamic GetMovieGenres()
        {
            throw new NotImplementedException();
        }

        internal object SelectMovieByMovieByGenres(int gID)
        {
            throw new NotImplementedException();
        }
    }
}