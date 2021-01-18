using System;

namespace Contracts
{
    public interface WeatherForecastUpdated
    {
        DateTime Date { get; }

        int TemperatureC { get; }

        int TemperatureF { get; }

        string Summary { get; }
    }
}
