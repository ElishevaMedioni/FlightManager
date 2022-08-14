using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class HeDateParts
    {
        public string y { get; set; }
        public string m { get; set; }
        public string d { get; set; }
    }

    public class DateRoot
    {
        public int gy { get; set; }
        public int gm { get; set; }
        public int gd { get; set; }
        public bool afterSunset { get; set; }
        public int hy { get; set; }
        public string hm { get; set; }
        public int hd { get; set; }
        public string hebrew { get; set; }
        public HeDateParts heDateParts { get; set; }
        public List<string> events { get; set; }

        public override string ToString()
        {
            return "heb-date"+
                ": "+hy.ToString()
                +"/"+hm.ToString()
                +"/" + hd.ToString();

        }
    }


}
