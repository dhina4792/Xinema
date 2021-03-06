﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using XinemaActual.DAL;
using System.Web.Mvc;
using System.Web.Helpers;
using System.Data;
using XinemaActual.Models;

namespace XinemaActual.Controllers
{
    public class MovieReviewController : Controller
    {
        private MovieReviewGateway movieReviewGateway;
        private MovieGateway moviesGateway;
        // GET: Showtimes
        public MovieReviewController()
        {
            movieReviewGateway = new MovieReviewGateway();
            moviesGateway = new MovieGateway();
            ViewBag.MovieGenreDropDownListItems = moviesGateway.GetMovieGenres();

        }

        public ActionResult Index(int? gID)
        {
            string[] genres = { };
            double[] imdbs = { };
            double[] avg = { };

            List<String> genre = new List<string>();
            List<Double> imdb = new List<Double>();
            List<Double> average = new List<Double>();

            double imdbTotal = 0;
            double rottenTotal = 0;
            double reviewAvg = 0;

            // Search through Genres to check for null or N/A and assign 0 if they are as said
            foreach (Movie s in moviesGateway.SelectAllGenres())
            {
                if (((s.movieIMDBRating != null) && (s.movieIMDBRating != "N/A")) || ((s.movieTomatoesRating != null) && (s.movieTomatoesRating != "N/A")))
                {
                    // store all genres for x axis
                    genre.Add(s.movieGenre);
                }

                // store all average for y axis
                if (s.movieIMDBRating == "N/A" || s.movieIMDBRating == null)
                {
                    imdbTotal += 0;
                }
                else
                {
                    imdbTotal += double.Parse(s.movieIMDBRating);
                }

                if (s.movieTomatoesRating == "N/A" || s.movieTomatoesRating == null)
                {
                    rottenTotal += 0;
                }
                else
                {
                    rottenTotal += double.Parse(s.movieTomatoesRating);
                }
                reviewAvg = ((imdbTotal + rottenTotal) / 2);
                //Response.Write(reviewAvg + "<br/>");

                if (reviewAvg != 0)
                {
                    average.Add(reviewAvg);
                }
                //Reset count after calculating each
                reviewAvg = 0;
                imdbTotal = 0;
                rottenTotal = 0;
            }

            // converting list to array
            genres = genre.ToArray();
            avg = average.ToArray();

            string themeChart = @"<Chart>
                      <ChartAreas>
                        <ChartArea Name=""Default"" _Template_=""All"">
                          <AxisY>
                            <LabelStyle Font=""Verdana, 12px"" />
                          </AxisY>
                          <AxisX LineColor=""64, 64, 64, 64"" Interval=""1"">
                            <LabelStyle Font=""Verdana, 12px"" />
                          </AxisX>
                        </ChartArea>
                      </ChartAreas>
                    </Chart>";

            var barChart = new Chart(width: 1200, height: 400, theme:themeChart)
                     .AddTitle("IMDB combined with Rotten Tomatoes Review for Each Genre")
                     .AddSeries(
                         name: "Genres",
                         //xValue: new[] { genres},
                         xValue: genres,
                         yValues: avg);
            barChart.Save("~/Content/genreChart.jpg", "jpeg");

             if (gID != null)
            {
                foreach (var item in ViewBag.MovieGenreDropDownListItems)
                {
                    if (item.Value == gID.ToString())
                    {
                        item.Selected = true;
                    }
                }
                return View(moviesGateway.SelectMovieByMovieByGenres((int)gID));

            }
            else
            {
                return View(moviesGateway.SelectAllMovies());
            }



        }
        public ActionResult Details(int? id)
        {
            // Checking if Review from IMDB or Rotten Tomatoes is null
            string imdb;
            string rotten;

            if (moviesGateway.SelectById(id).movieIMDBRating != null) { 
                if (moviesGateway.SelectById(id).movieIMDBRating.ToString() != "N/A") {
                    imdb = moviesGateway.SelectById(id).movieIMDBRating;
                }
                else
                {
                    imdb = null;
                }
            }else
            {
                imdb = null;

            }

            if (moviesGateway.SelectById(id).movieTomatoesRating != null)
            {
                if (moviesGateway.SelectById(id).movieTomatoesRating.ToString() != "N/A")
                {
                    rotten = moviesGateway.SelectById(id).movieTomatoesRating;
                }
                else
                {
                    rotten = null;
                }
            }
            else
            {
                rotten = null;

            }

            if ((imdb != null) && (rotten != null))
            {
                    var barChart = new Chart(width: 600, height: 400)
                     .AddTitle("Movie Review Rating for " + moviesGateway.SelectById(id).movieTitle)
                     .AddSeries(
                         name: "Movie",
                         xValue: new[] { "IMDB", "Rotten Tomatoes", "Overall" },
                         yValues: new[] { moviesGateway.SelectById(id).movieIMDBRating, moviesGateway.SelectById(id).movieTomatoesRating,
                        ((double.Parse(moviesGateway.SelectById(id).movieIMDBRating) + double.Parse(moviesGateway.SelectById(id).movieTomatoesRating))/2).ToString() });
                    barChart.Save("~/Content/barChart.jpg", "jpeg");
            }
            else
            {
                string imageFilePath = Server.MapPath(@"~/Content/barChart.jpg");
                System.IO.File.Delete(imageFilePath);
                ViewBag.message = "Ratings have not been given for this movie yet";
            }            // Return the contents of the Stream to the client

            return View(moviesGateway.SelectById(id));

        }
        public ActionResult barChart()
        {
            return base.File("~/Content/barChart.jpg", "jpeg");

        }
        public ActionResult genreChart()
        {

            return base.File("~/Content/genreChart.jpg", "jpeg");

        }

    }
}   