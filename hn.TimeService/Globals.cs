using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace CardService
{
    public class Globals
    {
        public string Debug
        {
            get
            {
                return ConfigurationManager.AppSettings["debug"];
            }
        }

        public string Time
        {
            get
            {
                return ConfigurationManager.AppSettings["time"];
            }
        }
    }
}
