using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineForAll.Libraries
{
    public class Citizen
    {
        public string CitizenEmail { get; set; }
        public string CitizenDistrictID { get; set; }
        public string CitizenDistrictName { get; set; }
        public string CitizenAge { get; set; }

        public Citizen()
        {
        }

        public Citizen(String citizenEmail, String citizenDistrictID, String citizenDistrictName, String citizenAge)
        {
            CitizenEmail = citizenEmail;
            CitizenDistrictID = citizenDistrictID;
            CitizenDistrictName = citizenDistrictName;
            CitizenAge = citizenAge;
        }
    }
}
