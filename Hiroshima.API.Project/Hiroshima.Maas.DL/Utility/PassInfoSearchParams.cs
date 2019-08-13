using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Hiroshima.Maas.DL.Utility
{
    public class PassInfoSearchParams
    {
        public string DeviceId { get; set; }
        public int TravellerId { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Lang { get; set; }
        public DateTime? StartDateAndTime { get; set; }
        public DateTime? EndDateAndTime { get; set; }
        public object[] GetSearchQuery(PassInfoSearchParams searchParams)
        {
            ArrayList paramList = new ArrayList();
            int paramCount = 0;
            StringBuilder queryString = new StringBuilder();

            if (StartDateAndTime != null)
            {
                queryString.Append(" and BookingDate >= @" + paramCount);
                paramList.Add(StartDateAndTime.Value.Date);
                paramCount++;
            }
            if (EndDateAndTime != null)
            {
                queryString.Append(" and BookingDate <= @" + paramCount);
                paramList.Add(EndDateAndTime.Value.Date);
                paramCount++;
            }

            //Create the array
            object[] queryObject = new object[2];
            //Remove the first 5 characters
            queryObject[0] = queryString.ToString().Substring(5);
            queryObject[1] = paramList;
            return queryObject;
        }
    }
}