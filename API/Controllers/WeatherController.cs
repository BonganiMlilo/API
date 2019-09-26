using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using API.Models;
using Newtonsoft.Json;

namespace API.Controllers
{
    public class WeatherController : Controller
    {
        // GET: Weather
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public async System.Threading.Tasks.Task<IActionResult> GetWeather(string city)
        {
            IActionResult _result = new ObjectResult(false);
            GenericResult _authenticationResult = null;
            var Weather = new object();
            try
            {
                string url = "http://api.openweathermap.org/data/2.5/weather?q=";
                string units_appid = "&units=metric&appid=301955a589d76e0addaa307593cc81ab";
                string uri = url + city + units_appid;
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(uri);

                //will throw an exception if not successful
                if (response != null)
                {
                    response.EnsureSuccessStatusCode();
                    string content = await response.Content.ReadAsStringAsync();

                    dynamic dynJson = JsonConvert.DeserializeObject(content);

                    Weather = dynJson;

                    _result = new ObjectResult(Weather);
                }
                else if (response == null)
                {
                    _authenticationResult = new GenericResult()
                    {
                        Succeeded = false,
                        Message = "failed",

                    };
                    _result = new ObjectResult(_authenticationResult);
                }

            }
            catch (Exception ex)
            {
                _authenticationResult = new GenericResult()
                {
                    Succeeded = false,
                    Message = ex.Message
                };

            }

            return _result;
        }
    }

    internal class ObjectResult : IActionResult
    {
        private bool v;

        public ObjectResult(bool v)
        {
            this.v = v;
        }
    }
}