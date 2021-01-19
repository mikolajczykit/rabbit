using System;

namespace Contracts
{
    public interface WeatherForecastUpdated
    {
        DateTime Date { get; set; }

        int TemperatureC { get; set; }

        string Summary { get; set; }
    }
}
