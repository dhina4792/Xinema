using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using XinemaActual.Models;
using OpenQA.Selenium.Support.UI;

namespace XinemaActual.DataScrapingLogic
{
    public class IMDBReviewScraper
    {
        private string targetURL;
        private string targetMovie;
        IWebDriver driver;
        WebDriverWait wait;
        public IMDBReviewScraper()
        {
            targetURL = "http://www.imdb.com/";
            driver = new PhantomJSDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public MovieReview getIMDBRating(string movieName)
        {
            targetMovie = parseMovieText(movieName);
            Console.WriteLine("Scrapping review for " + targetMovie);
            MovieReview moviereview = new MovieReview();
            //Notice navigation is slightly different than the Java version
            //This is because 'get' is a keyword in C#
            driver.Navigate().GoToUrl(targetURL);
            // Find the text input element by its name
            IWebElement query = driver.FindElement(By.Name("q"));
            // Enter something to search for
            query.SendKeys(targetMovie);
            // Now submit the form. WebDriver will find the form for us from the element          
            query.Submit();


            // Google's search is rendered dynamically with JavaScript.
            // Wait for the page to load, timeout after 10 seconds
            try
            {

                IWebElement myDynamicElement = wait.Until((d) =>
                {
                    return d.FindElement(By.ClassName("findSection"));
                });

                //get all links
                IList<IWebElement> links = driver.FindElements(By.TagName("a"));
                // iterate all links till found moviename
                foreach (var link in links)
                {
                    if ((link.Text.ToLower().Contains(targetMovie.ToLower())) || (targetMovie.Contains(link.Text)))
                    {

                        var imdbRating = driver.FindElement(By.CssSelector("span[itemprop^='ratingValue']")).Text;
                        moviereview.movieReviewIMDB = imdbRating;
                        break;

                    }

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

            }

            return moviereview;
        }

        private string parseMovieText(string text)
        {
            string[] words = text.Split('(');
            return words[0];
        }
    }
}