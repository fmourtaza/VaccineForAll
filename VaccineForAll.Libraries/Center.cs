using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineForAll.Libraries
{
    public class Center
    {
        public int center_id { get; set; }
        public string name { get; set; }
        public string name_l { get; set; }
        public string address { get; set; }
        public string address_l { get; set; }
        public string state_name { get; set; }
        public string state_name_l { get; set; }
        public string district_name { get; set; }
        public string district_name_l { get; set; }
        public string block_name { get; set; }
        public string block_name_l { get; set; }
        public string pincode { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string fee_type { get; set; }
        public IList<VaccineFee> vaccine_fees { get; set; }
        public IList<Session> sessions { get; set; }
    }
}
