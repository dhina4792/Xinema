using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XinemaActual.Models;

namespace XinemaActual.DAL
{
    public class ShowtimeGateway : DataGateway<Showtime>
    {
        private List<SelectListItem> cinemaItems = null;
        private List<SelectListItem> dateItems = null;
        private List<SelectListItem> movieItems = null;

        internal dynamic GetCinemaNames()
        {
            cinemaItems = new List<SelectListItem>();
            var cinemas = data.GroupBy(t => t.showtimeCinemaName).ToArray();
            int i = 0;
            cinemaItems.Add(new SelectListItem { Text = "All", Value = null, Selected = true });

            foreach (var item in cinemas)
            {
                
                cinemaItems.Add(new SelectListItem { Text = item.Key.ToString(), Value = i.ToString(), Selected = false });
                i++;
            }

            return cinemaItems;

        }

        internal dynamic GetShowtimeMovies()
        {
            movieItems = new List<SelectListItem>();
            var movies = data.GroupBy(t => t.showtimeTitle).ToArray();
            int i = 0;
            movieItems.Add(new SelectListItem { Text = "All", Value = null, Selected = true });

            foreach (var item in movies)
            {

                movieItems.Add(new SelectListItem { Text = item.Key.ToString(), Value = i.ToString(), Selected = false });
                i++;
            }

            return movieItems;
        }

        internal dynamic GetShowtimeDates()
        {
            dateItems = new List<SelectListItem>();
            var dates = data.GroupBy(t => t.showtimeDate).ToArray();
            int i = 0;
            dateItems.Add(new SelectListItem { Text = "All", Value = i.ToString(), Selected = false });
            foreach (var item in dates)
            {

                i++;
                DateTime d;
                DateTime.TryParseExact(item.Key.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out d);
                if (d.Date == DateTime.Now.Date)
                {
                    dateItems.Add(new SelectListItem { Text = item.Key.ToString(), Value = null, Selected = true });
                    
                }
                else
                {
                    dateItems.Add(new SelectListItem { Text = item.Key.ToString(), Value = i.ToString(), Selected = false });
                }

                
            }

            return dateItems;
        }
        internal dynamic SelectShowtimeByCinemaNameAndCurrentDate(int cID)
        {
            string cinemaName = cinemaItems.Where(item => item.Value == cID.ToString()).First().Text;
            return data.Where(t =>t.showtimeCinemaName == cinemaName && t.showtimeDate == currDate);
        }
        internal dynamic SelectShowtimeByMovieAndCurrentDate(int mID)
        {
            string movieName = movieItems.Where(item => item.Value == mID.ToString()).First().Text;
            return data.Where(t => t.showtimeTitle == movieName && t.showtimeDate == currDate);
        }
        internal dynamic SelectShowtimeByDate(int dID)
        {
            if(dID != 0)
            {
                string date = dateItems.Where(item => item.Value == dID.ToString()).First().Text;
                return data.Where(t => t.showtimeDate == date);
            }
            return data;

        }
        internal dynamic SelectAllShowtimes()
        {
            
            return data.Where(t => t.showtimeDate == currDate);
        }
    }
}