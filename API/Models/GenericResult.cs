using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class GenericResult
    {
        private object weather;

        public GenericResult(object weather)
        {
            this.weather = weather;
        }

        public bool Succeeded { get; set; }
        public string Message { get; set; }
    }
}