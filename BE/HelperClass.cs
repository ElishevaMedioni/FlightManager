using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class HelperClass
    {
        //helper function to convert from Unix time to human datetime
        public DateTime GetTimeFromEpoch(double EpochTimeStamp)
        {
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, 0);//fromstart epoch time
            start = start.AddSeconds(EpochTimeStamp);//add the seconds to the start DateTime
            return start;
        }
        public double GetTemp(double kelvin)
        {
            double cel = kelvin -273.15;
            return cel;
        }
    }
}
