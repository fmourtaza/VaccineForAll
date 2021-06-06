using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VaccineForAll.Libraries
{
    public class CitizenMobile
    {
        public string Mobile { get; set; }

        public CitizenMobile(string mobile)
        {
            Mobile = mobile;
        }

        public string GetJSon()
        {
            string jsonToReturn = Regex.Unescape(JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented));
            return jsonToReturn;
        }
    }
}
