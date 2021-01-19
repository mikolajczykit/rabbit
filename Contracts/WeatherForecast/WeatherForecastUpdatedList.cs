using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public class WeatherForecastUpdatedList
    {
        public WeatherForecastUpdatedList() 
        {
            this.WeatherForecastUpdatedDays = new List<WeatherForecastUpdated>();
        }

        public WeatherForecastUpdatedList(List<WeatherForecastUpdated> list)
        {
            this.WeatherForecastUpdatedDays = list;
        }

        public List<WeatherForecastUpdated> WeatherForecastUpdatedDays { get; set; }
    }
}
