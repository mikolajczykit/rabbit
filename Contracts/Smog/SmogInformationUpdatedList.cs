using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public class SmogInformationUpdatedList
    {
        public SmogInformationUpdatedList() 
        {
            this.SmogInformationUpdatedDays = new List<SmogInformationUpdated>();
        }

        public SmogInformationUpdatedList(List<SmogInformationUpdated> list)
        {
            this.SmogInformationUpdatedDays = list;
        }

        public List<SmogInformationUpdated> SmogInformationUpdatedDays { get; set; }
    }
}
