﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDAL
    {
        #region FlightInfoPartial
         Dictionary<string, List<BE.FlightInfoPartial>> GetCurrentFlights();

        #endregion FlightInfoPartial

    }
}
