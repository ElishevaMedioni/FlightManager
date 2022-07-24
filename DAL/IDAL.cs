using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDAL
    {
        #region FlightInfoPartial
        void AddFlightInfoPartialToDB(BE.FlightInfoPartial newFlightInfoPartial);
        BE.FlightInfoPartial GetFlightInfoPartial(Func<BE.FlightInfoPartial, bool> predicate = null);

        void UpdateFlightInfoPartial(BE.FlightInfoPartial updateFlightInfoPartial);

        List<BE.FlightInfoPartial> GetAllFlightInfoPartial(Func<BE.FlightInfoPartial, bool> predicate = null);

        #endregion FlightInfoPartial
    }
}
