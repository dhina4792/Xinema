using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XinemaActual.Models;

namespace XinemaActual.DAL
{
    public class MovieReviewGateway : DataGateway<MovieReview>
    {
        public IEnumerable<MovieReview> SelectAllMoviesReviews()
        {
            IEnumerable<MovieReview> groupedMoviesReviews = data.GroupBy(t => t.movieReviewName).Select(t => t.FirstOrDefault());
            return groupedMoviesReviews;

        }
    }
}