using System;

namespace Contracts
{
    public interface SmogInformationUpdated
    {
        DateTime Date { get; set; }

        string Summary { get; set; }
    }
}
