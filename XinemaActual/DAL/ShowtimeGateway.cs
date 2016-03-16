using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XinemaActual.Models;
using XinemaActual.ViewModels;

namespace XinemaActual.DAL
{
    public class ShowtimeGateway : DataGateway<Showtime>
    {
        public List<ShowtimeVM> GetallShowtimes(MovieGateway movies,CinemaGateway cinemas)
        {
            //MovieGateway movies = new MovieGateway();
            //CinemaGateway cinemas = new CinemaGateway();
            var cinemasList = cinemas.SelectAll();
            var moviesList = movies.SelectAll();
            var showtimesList = from t in SelectAll()
                                select t;

            var viewmodels = (from s in showtimesList
                              join c in cinemasList on s.cinemas_cinemasID equals c.cinemaID
                              join m in moviesList on s.movies_moviesID equals m.movieID
                              where DateTime.ParseExact(s.showtimeDate, "dd/MM/yyyy", null) == DateTime.Now.Date
                              select new ShowtimeVM()
                              {
                                  showtimeStartTime = s.showtimeStartTime,
                                  cinemaName = c.cinemaName,
                                  showtimeDate = s.showtimeDate,
                                  movieTitle = m.movieTitle
                                  
                             }).ToList();
            return viewmodels;
        }
    }
}