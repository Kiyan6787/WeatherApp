using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Cities
    {
        public int Id { get; set; }
        public string cityName { get; set; }
        public int temperature { get; set; }
        public string weatherType { get; set; }
        public int Humidity { get; set; }
        public int WindSpeed { get; set; }

        public Cities()
        {

        }
    }
}